import 'package:flutter/material.dart';
import 'package:universitymobileapp/app/studentDetailsScreen.dart';
import '../data/StudentService.dart';
import '../data/connection.dart';

class StudentsScreen extends StatefulWidget {
  const StudentsScreen({super.key});

  @override
  State<StudentsScreen> createState() => _StudentsScreenState();
}

class _StudentsScreenState extends State<StudentsScreen> {
  final StudentService studentService = StudentService();
  late Future<List<dynamic>> students;

  @override
  void initState() {
    super.initState();
    students = studentService.getStudents();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Students'),
        backgroundColor: Colors.indigo,
      ),
      body: FutureBuilder<List<dynamic>>(
        future: students,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const Center(child: CircularProgressIndicator());
          }

          if (snapshot.hasError) {
            return Center(child: Text('Error: ${snapshot.error}'));
          }

          final studentsList = snapshot.data ?? [];

          if (studentsList.isEmpty) {
            return const Center(child: Text('No Students Found'));
          }

          return ListView.builder(
            padding: const EdgeInsets.all(12),
            itemCount: studentsList.length,
            itemBuilder: (context, index) {
              final student = studentsList[index];

              final String imagePath = student['imageUrl']?.toString() ?? '';
              final String imageUrl = imagePath.isEmpty
                  ? ''
                  : Uri.parse(ConnectionString.dashboard)
                  .resolve(imagePath)
                  .toString();

              return Card(
                elevation: 3,
                margin: const EdgeInsets.symmetric(vertical: 8),
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(12),
                ),
                child: ListTile(
                  leading: ClipRRect(
                    borderRadius: BorderRadius.circular(6),
                    child: imageUrl.isNotEmpty
                        ? Image.network(
                      imageUrl,
                      width: 70,
                      height: 50,
                      fit: BoxFit.cover,
                      errorBuilder: (context, error, stackTrace) {
                        return Container(
                          width: 70,
                          height: 50,
                          color: Colors.grey[200],
                          child: const Icon(Icons.badge, size: 30),
                        );
                      },
                    )
                        : Container(
                      width: 70,
                      height: 50,
                      color: Colors.grey[200],
                      child: const Icon(Icons.badge, size: 30),
                    ),
                  ),
                  title: Text(
                    student['name']?.toString() ?? '',
                    style: const TextStyle(fontWeight: FontWeight.bold),
                  ),
                  subtitle: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Text(student['email']?.toString() ?? ''),
                      Text(
                        'Program: ${student['degreeProgramName'] ?? 'N/A'}',
                        style: const TextStyle(color: Colors.blue, fontSize: 12),
                      ),
                    ],
                  ),
                  trailing: const Icon(Icons.arrow_forward_ios, size: 16),
                  onTap: () {
                    Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (context) => StudentDetailsScreen(student),
                      ),
                    );
                  },
                ),
              );
            },
          );
        },
      ),
    );
  }
}