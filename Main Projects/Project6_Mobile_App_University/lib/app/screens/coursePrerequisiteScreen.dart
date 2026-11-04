import 'package:flutter/material.dart';
import '../../data/coursePrerequisiteService.dart';
import 'detailScreens/coursePrerequisiteDetailsScreen.dart';

class CoursePrerequisiteScreen extends StatefulWidget {
  const CoursePrerequisiteScreen({super.key});

  @override
  State<CoursePrerequisiteScreen> createState() => _CoursePrerequisiteScreenState();
}

class _CoursePrerequisiteScreenState extends State<CoursePrerequisiteScreen> {
  final CoursePrerequisiteService prerequisiteService = CoursePrerequisiteService();
  late Future<List<dynamic>> prerequisites;

  @override
  void initState() {
    super.initState();
    prerequisites = prerequisiteService.getCoursePrerequisites();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Course Prerequisites'),
        backgroundColor: Colors.indigo,
      ),
      body: FutureBuilder<List<dynamic>>(
        future: prerequisites,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const Center(child: CircularProgressIndicator());
          }

          if (snapshot.hasError) {
            return Center(child: Text('Error: ${snapshot.error}'));
          }

          final prereqsList = snapshot.data ?? [];

          if (prereqsList.isEmpty) {
            return const Center(child: Text('No Course Prerequisites Found'));
          }

          return ListView.builder(
            padding: const EdgeInsets.all(12),
            itemCount: prereqsList.length,
            itemBuilder: (context, index) {
              final prereq = prereqsList[index];

              return Card(
                elevation: 3,
                margin: const EdgeInsets.symmetric(vertical: 8),
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(12),
                ),
                child: ListTile(
                  contentPadding: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
                  title: Text(
                    'Course: ${prereq['course']?.toString() ?? 'N/A'}',
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
                        Text('Prerequisite Course ID: ${prereq['prerequisiteCourseId']?.toString() ?? 'N/A'}'),
                        const SizedBox(height: 2),
                        Text(
                          'Min Grade Required: ${prereq['minGradeRequired']?.toString() ?? 'N/A'}',
                          style: const TextStyle(color: Colors.green, fontWeight: FontWeight.bold),
                        ),
                      ],
                    ),
                  ),
                  trailing: const Icon(Icons.arrow_forward_ios, size: 16),
                  onTap: () {
                    Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (context) => CoursePrerequisiteDetailsScreen(prereq),
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