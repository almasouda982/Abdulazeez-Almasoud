import 'package:flutter/material.dart';
import 'package:universitymobileapp/data/CourseService.dart';

import '../../data/connection.dart';
import 'detailScreens/courseDetailsScreen.dart';

class CoursesScreen extends StatefulWidget {
  const CoursesScreen({super.key});

  @override
  State<CoursesScreen> createState() => _CoursesScreenState();
}

class _CoursesScreenState extends State<CoursesScreen> {
  final CourseService courseService = CourseService();
  late Future<List<dynamic>> courses;

  void initState(){
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

              final String imagePath = course['imageUrl']?.toString() ?? '';
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
                    course['title']?.toString() ?? '',
                    style: const TextStyle(fontWeight: FontWeight.bold),
                  ),
                  subtitle: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Text(course['code']?.toString() ?? ''),
                      Text(
                        'Credits: ${course['credits'] ?? 'N/A'}',
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
