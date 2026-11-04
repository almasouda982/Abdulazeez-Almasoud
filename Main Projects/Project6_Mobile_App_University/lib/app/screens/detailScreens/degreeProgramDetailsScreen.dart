import 'package:flutter/material.dart';

class DegreeProgramDetailsScreen extends StatelessWidget {
  final dynamic degreeProgram;

  const DegreeProgramDetailsScreen(this.degreeProgram, {super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Degree Program Details'),
        backgroundColor: Colors.indigo,
      ),
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(20),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                degreeProgram['name']?.toString() ?? 'N/A',
                style: const TextStyle(
                  fontSize: 24,
                  fontWeight: FontWeight.bold,
                ),
              ),
              const SizedBox(height: 16),
              Text(
                'Catalog Year: ${degreeProgram['catalogYear']?.toString() ?? 'N/A'}',
                style: const TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w500,
                  color: Colors.indigo,
                ),
              ),
              const SizedBox(height: 8),
              Text(
                'Total Credits Required: ${degreeProgram['totalCreditsRequired']?.toString() ?? 'N/A'}',
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