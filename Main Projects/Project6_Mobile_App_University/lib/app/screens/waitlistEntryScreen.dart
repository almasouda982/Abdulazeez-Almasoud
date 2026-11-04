import 'package:flutter/material.dart';
import '../../data/waitlistEntryService.dart';
import 'detailScreens/waitlistEntryDetailsScreen.dart';

class WaitlistEntryScreen extends StatefulWidget {
  const WaitlistEntryScreen({super.key});

  @override
  State<WaitlistEntryScreen> createState() => _WaitlistEntryScreenState();
}

class _WaitlistEntryScreenState extends State<WaitlistEntryScreen> {
  final WaitlistEntryService waitlistService = WaitlistEntryService();
  late Future<List<dynamic>> waitlistEntries;

  @override
  void initState() {
    super.initState();
    waitlistEntries = waitlistService.getWaitlistEntrys();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Waitlist Entries'),
        backgroundColor: Colors.indigo,
      ),
      body: FutureBuilder<List<dynamic>>(
        future: waitlistEntries,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const Center(child: CircularProgressIndicator());
          }

          if (snapshot.hasError) {
            return Center(child: Text('Error: ${snapshot.error}'));
          }

          final entriesList = snapshot.data ?? [];

          if (entriesList.isEmpty) {
            return const Center(child: Text('No Waitlist Entries Found'));
          }

          return ListView.builder(
            padding: const EdgeInsets.all(12),
            itemCount: entriesList.length,
            itemBuilder: (context, index) {
              final entry = entriesList[index];

              return Card(
                elevation: 3,
                margin: const EdgeInsets.symmetric(vertical: 8),
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(12),
                ),
                child: ListTile(
                  contentPadding: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
                  title: Text(
                    'Student: ${entry['student']?.toString() ?? 'N/A'}',
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
                        Text('Section: ${entry['courseSection']?.toString() ?? 'N/A'}'),
                        const SizedBox(height: 2),
                        Text(
                          'Position: ${entry['position']?.toString() ?? 'N/A'}',
                          style: const TextStyle(color: Colors.indigo, fontWeight: FontWeight.bold),
                        ),
                        const SizedBox(height: 2),
                        Text('Status: ${entry['status']?.toString() ?? 'N/A'}'),
                      ],
                    ),
                  ),
                  trailing: const Icon(Icons.arrow_forward_ios, size: 16),
                  onTap: () {
                    Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (context) => WaitlistEntryDetailsScreen(entry),
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