import 'package:flutter/material.dart';
import 'package:universitymobileapp/app/screens/detailScreens/enrollmentDetailsScreen.dart';
import '../../data/EnrollmentService.dart';
import '../../data/connection.dart';

class EnrollmentScreen extends StatefulWidget {
  const EnrollmentScreen({super.key});

  @override
  State<EnrollmentScreen> createState() => _EnrollmentScreenState();
}

class _EnrollmentScreenState extends State<EnrollmentScreen> {
  final EnrollmentService enrollmentService = EnrollmentService();
  late Future<List<dynamic>> enrollments;

  @override
  void initState() {
    super.initState();
    enrollments = enrollmentService.getEnrollments();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Enrollments'),
        backgroundColor: Colors.indigo,
      ),
      body: FutureBuilder<List<dynamic>>(
        future: enrollments,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const Center(child: CircularProgressIndicator());
          }

          if (snapshot.hasError) {
            return Center(child: Text('Error: ${snapshot.error}'));
          }

          final enrollmentsList = snapshot.data ?? [];

          if (enrollmentsList.isEmpty) {
            return const Center(child: Text('No Enrollments Found'));
          }

          return ListView.builder(
            padding: const EdgeInsets.all(12),
            itemCount: enrollmentsList.length,
            itemBuilder: (context, index) {
              final enrollment = enrollmentsList[index];

              return Card(
                elevation: 3,
                margin: const EdgeInsets.symmetric(vertical: 8),
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(12),
                ),
                child: ListTile(
                  contentPadding: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
                  title: Text(
                    enrollment['lastName']?.toString() ?? 'N/A',
                    style: const TextStyle(
                      fontWeight: FontWeight.bold,
                      fontSize: 16,
                    ),
                  ),
                  subtitle: Padding(
                    padding: const EdgeInsets.only(top: 6.0),
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text('Section: ${enrollment['sectionNumber']?.toString() ?? 'N/A'}'),
                        const SizedBox(height: 2),
                        Text(
                          'Final Grade: ${enrollment['finalGrade'] ?? 'N/A'}',
                          style: const TextStyle(color: Colors.green, fontWeight: FontWeight.bold),
                        ),
                        const SizedBox(height: 2),
                        Text('Status: ${enrollment['status']?.toString() ?? 'N/A'}'),
                      ],
                    ),
                  ),
                  trailing: const Icon(Icons.arrow_forward_ios, size: 16),
                  onTap: () {
                    Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (context) => EnrollmentDetailsScreen(enrollment),
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