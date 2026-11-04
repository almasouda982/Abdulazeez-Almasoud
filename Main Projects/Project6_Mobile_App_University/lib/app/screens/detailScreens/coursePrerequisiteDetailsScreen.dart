import 'package:flutter/material.dart';

class CoursePrerequisiteDetailsScreen extends StatelessWidget {
  final dynamic prerequisite;

  const CoursePrerequisiteDetailsScreen(this.prerequisite, {super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Prerequisite Details'),
        backgroundColor: Colors.indigo,
      ),
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(20),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                'Course: ${prerequisite['course']?.toString() ?? 'N/A'}',
                style: const TextStyle(
                  fontSize: 24,
                  fontWeight: FontWeight.bold,
                ),
              ),
              const SizedBox(height: 16),
              Text(
                'Prerequisite Course ID: ${prerequisite['prerequisiteCourseId']?.toString() ?? 'N/A'}',
                style: const TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w500,
                  color: Colors.indigo,
                ),
              ),
              const SizedBox(height: 8),
              Text(
                'Min Grade Required: ${prerequisite['minGradeRequired']?.toString() ?? 'N/A'}',
                style: const TextStyle(
                  fontSize: 16,
                  color: Colors.green,
                  fontWeight: FontWeight.bold,
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}