import 'package:flutter/material.dart';
import '../../data/degreeRequirementService.dart';
import 'detailScreens/degreeRequirementDetailsScreen.dart';

class DegreeRequirementScreen extends StatefulWidget {
  const DegreeRequirementScreen({super.key});

  @override
  State<DegreeRequirementScreen> createState() => _DegreeRequirementScreenState();
}

class _DegreeRequirementScreenState extends State<DegreeRequirementScreen> {
  final DegreeRequirementService requirementService = DegreeRequirementService();
  late Future<List<dynamic>> requirements;

  @override
  void initState() {
    super.initState();
    requirements = requirementService.getDegreeRequirements();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Degree Requirements'),
        backgroundColor: Colors.indigo,
      ),
      body: FutureBuilder<List<dynamic>>(
        future: requirements,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const Center(child: CircularProgressIndicator());
          }

          if (snapshot.hasError) {
            return Center(child: Text('Error: ${snapshot.error}'));
          }

          final requirementsList = snapshot.data ?? [];

          if (requirementsList.isEmpty) {
            return const Center(child: Text('No Degree Requirements Found'));
          }

          return ListView.builder(
            padding: const EdgeInsets.all(12),
            itemCount: requirementsList.length,
            itemBuilder: (context, index) {
              final req = requirementsList[index];

              return Card(
                elevation: 3,
                margin: const EdgeInsets.symmetric(vertical: 8),
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(12),
                ),
                child: ListTile(
                  contentPadding: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
                  title: Text(
                    req['degreeProgram']?.toString() ?? 'N/A',
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
                        Text('Category: ${req['categoryName']?.toString() ?? 'N/A'}'),
                        const SizedBox(height: 2),
                        Text(
                          'Required Credits: ${req['requiredCredits']?.toString() ?? 'N/A'}',
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
                        builder: (context) => DegreeRequirementDetailsScreen(req),
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