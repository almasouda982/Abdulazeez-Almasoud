import 'package:flutter/material.dart';
import '../../data/courseSectionService.dart';
import 'detailScreens/courseSectionDetailsScreen.dart';

class CourseSectionScreen extends StatefulWidget {
  const CourseSectionScreen({super.key});

  @override
  State<CourseSectionScreen> createState() => _CourseSectionScreenState();
}

class _CourseSectionScreenState extends State<CourseSectionScreen> {
  final CourseSectionService sectionService = CourseSectionService();
  late Future<List<dynamic>> courseSections;

  @override
  void initState() {
    super.initState();
    courseSections = sectionService.getCourseSections();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Course Sections'),
        backgroundColor: Colors.indigo,
      ),
      body: FutureBuilder<List<dynamic>>(
        future: courseSections,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const Center(child: CircularProgressIndicator());
          }

          if (snapshot.hasError) {
            return Center(child: Text('Error: ${snapshot.error}'));
          }

          final sectionsList = snapshot.data ?? [];

          if (sectionsList.isEmpty) {
            return const Center(child: Text('No Course Sections Found'));
          }

          return ListView.builder(
            padding: const EdgeInsets.all(12),
            itemCount: sectionsList.length,
            itemBuilder: (context, index) {
              final section = sectionsList[index];

              return Card(
                elevation: 3,
                margin: const EdgeInsets.symmetric(vertical: 8),
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(12),
                ),
                child: ListTile(
                  contentPadding: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
                  title: Text(
                    'Course: ${section['course']?.toString() ?? 'N/A'}',
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
                        Text('Semester: ${section['semester']?.toString() ?? 'N/A'}'),
                        const SizedBox(height: 2),
                        Text(
                          'Section Number: ${section['sectionNumber']?.toString() ?? 'N/A'}',
                          style: const TextStyle(color: Colors.indigo, fontWeight: FontWeight.bold),
                        ),
                        const SizedBox(height: 2),
                        Text('Time: ${section['timeSlot']?.toString() ?? 'N/A'} (${section['daysOfWeek']?.toString() ?? 'N/A'})'),
                      ],
                    ),
                  ),
                  trailing: const Icon(Icons.arrow_forward_ios, size: 16),
                  onTap: () {
                    Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (context) => CourseSectionDetailsScreen(section),
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