import 'package:flutter/material.dart';
import '../../../data/connection.dart';

class EnrollmentDetailsScreen extends StatelessWidget {
  final dynamic enrollment;

  const EnrollmentDetailsScreen(this.enrollment, {super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Enrollment Details'),
        backgroundColor: Colors.indigo,
      ),
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(20),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                'Last Name: ${enrollment['lastName']?.toString() ?? 'N/A'}',
                style: const TextStyle(
                  fontSize: 24,
                  fontWeight: FontWeight.bold,
                ),
              ),
              const SizedBox(height: 16),
              Text(
                'Section Number: ${enrollment['sectionNumber']?.toString() ?? 'N/A'}',
                style: const TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w500,
                  color: Colors.indigo,
                ),
              ),
              const SizedBox(height: 8),
              Text(
                'Final Grade: ${enrollment['finalGrade'] ?? 'N/A'}',
                style: const TextStyle(
                  fontSize: 16,
                  color: Colors.green,
                  fontWeight: FontWeight.bold,
                ),
              ),
              const SizedBox(height: 8),
              Text(
                'Letter Grade Points: ${enrollment['letterGradePoints']?.toString() ?? 'N/A'}',
                style: const TextStyle(fontSize: 16),
              ),
              const SizedBox(height: 8),
              Text(
                'Status: ${enrollment['status']?.toString() ?? 'N/A'}',
                style: const TextStyle(fontSize: 16),
              ),
              const SizedBox(height: 8),
              InkWell(
                onTap: () {
                  final String docPath = enrollment['documentUrl']?.toString() ?? '';
                  final String documentUrl = docPath.isEmpty
                      ? ''
                      : Uri.parse(ConnectionString.dashboard).resolve(docPath).toString();

                  ScaffoldMessenger.of(context).showSnackBar(
                    SnackBar(content: Text('Opening: $documentUrl')),
                  );
                },
                child: Text(
                  '${enrollment['lastName'] ?? 'Student'}.transcript',
                  style: const TextStyle(
                    fontSize: 16,
                    color: Colors.blue,
                    fontWeight: FontWeight.bold,
                    decoration: TextDecoration.underline,
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}