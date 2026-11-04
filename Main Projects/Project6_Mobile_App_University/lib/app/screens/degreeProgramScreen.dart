import 'package:flutter/material.dart';
import '../../data/degreeProgramService.dart';
import 'detailScreens/degreeProgramDetailsScreen.dart';

class DegreeProgramScreen extends StatefulWidget {
  const DegreeProgramScreen({super.key});

  @override
  State<DegreeProgramScreen> createState() => _DegreeProgramScreenState();
}

class _DegreeProgramScreenState extends State<DegreeProgramScreen> {
  final DegreeProgramService degreeProgramService = DegreeProgramService();
  late Future<List<dynamic>> degreePrograms;

  @override
  void initState() {
    super.initState();
    degreePrograms = degreeProgramService.getDegreePrograms();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Degree Programs'),
        backgroundColor: Colors.indigo,
      ),
      body: FutureBuilder<List<dynamic>>(
        future: degreePrograms,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const Center(child: CircularProgressIndicator());
          }

          if (snapshot.hasError) {
            return Center(child: Text('Error: ${snapshot.error}'));
          }

          final programsList = snapshot.data ?? [];

          if (programsList.isEmpty) {
            return const Center(child: Text('No Degree Programs Found'));
          }

          return ListView.builder(
            padding: const EdgeInsets.all(12),
            itemCount: programsList.length,
            itemBuilder: (context, index) {
              final program = programsList[index];

              return Card(
                elevation: 3,
                margin: const EdgeInsets.symmetric(vertical: 8),
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(12),
                ),
                child: ListTile(
                  contentPadding: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
                  title: Text(
                    program['name']?.toString() ?? 'N/A',
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
                        Text('Catalog Year: ${program['catalogYear']?.toString() ?? 'N/A'}'),
                        const SizedBox(height: 2),
                        Text(
                          'Credits Required: ${program['totalCreditsRequired']?.toString() ?? 'N/A'}',
                          style: const TextStyle(color: Colors.indigo, fontWeight: FontWeight.bold),
                        ),
                      ],
                    ),
                  ),
                  trailing: const Icon(Icons.arrow_forward_ios, size: 16),
                  onTap: () {
                    Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (context) => DegreeProgramDetailsScreen(program),
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