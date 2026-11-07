import 'package:flutter/material.dart';
import 'package:universitymobileapp/data/CourseService.dart';
import 'detailScreens/courseDetailsScreen.dart';

class CoursesScreen extends StatefulWidget {
  const CoursesScreen({super.key});

  @override
  State<CoursesScreen> createState() => _CoursesScreenState();
}

class _CoursesScreenState extends State<CoursesScreen> {
  final CourseService courseService = CourseService();
  late Future<List<dynamic>> courses;

  @override
  void initState() {
    super.initState();
    courses = courseService.getCourses();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Courses'),
        backgroundColor: Colors.indigo,
      ),
      body: FutureBuilder<List<dynamic>>(
        future: courses,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const Center(child: CircularProgressIndicator());
          }

          if (snapshot.hasError) {
            return Center(child: Text('Error: ${snapshot.error}'));
          }

          final coursesList = snapshot.data ?? [];

          if (coursesList.isEmpty) {
            return const Center(child: Text('No Courses Found'));
          }

          return ListView.builder(
            padding: const EdgeInsets.all(12),
            itemCount: coursesList.length,
            itemBuilder: (context, index) {
              final course = coursesList[index];
              final books = course['books'] as List<dynamic>? ?? [];

              return Card(
                elevation: 3,
                margin: const EdgeInsets.symmetric(vertical: 8),
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(12),
                ),
                child: ListTile(
                  contentPadding: const EdgeInsets.all(12),
                  leading: Container(
                    width: 50,
                    height: 70,
                    decoration: BoxDecoration(
                      color: Colors.indigo.withOpacity(0.1),
                      borderRadius: BorderRadius.circular(6),
                    ),
                    child: const Icon(Icons.school, color: Colors.indigo, size: 30),
                  ),
                  title: Text(
                    course['title']?.toString() ?? '',
                    style: const TextStyle(fontWeight: FontWeight.bold),
                  ),
                  subtitle: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      const SizedBox(height: 4),
                      Text(course['code']?.toString() ?? ''),
                      const SizedBox(height: 2),
                      Text(
                        'Credits: ${course['credits'] ?? 'N/A'} | Books: ${books.length}',
                        style: const TextStyle(color: Colors.blue, fontSize: 12),
                      ),
                    ],
                  ),
                  trailing: const Icon(Icons.arrow_forward_ios, size: 16),
                  onTap: () {
                    Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (context) => CourseDetailsScreen(course),
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