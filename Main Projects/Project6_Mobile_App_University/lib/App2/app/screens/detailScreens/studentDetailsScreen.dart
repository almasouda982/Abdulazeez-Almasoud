import 'package:flutter/material.dart';
import '../../../data/connection.dart';

class StudentDetailsScreen extends StatelessWidget {
  final dynamic student;

  const StudentDetailsScreen(this.student, {super.key});

  @override
  Widget build(BuildContext context) {
    final String imagePath = student['imageUrl']?.toString() ?? '';
    final String imageUrl = imagePath.isEmpty
        ? ''
        : Uri.parse(ConnectionString.dashboard)
        .resolve(imagePath)
        .toString();

    final dynamic rawStatus = student['enrollmentStatus'];
    final String enrollmentStatusText = (rawStatus == true || rawStatus?.toString().toLowerCase() == 'true')
        ? 'Enrolled'
        : 'Not Enrolled';

    final double? gpa = double.tryParse(student['gpa']?.toString() ?? '');
    Color gpaColor = Colors.grey;
    if (gpa != null) {
      if (gpa >= 3.00) {
        gpaColor = Colors.green;
      } else if (gpa >= 2.00 && gpa <= 2.99) {
        gpaColor = Colors.amber.shade700;
      } else {
        gpaColor = Colors.red;
      }
    }

    return Scaffold(
      backgroundColor: Colors.grey[50],
      appBar: AppBar(
        title: const Text(
          'Student Profile',
          style: TextStyle(fontWeight: FontWeight.bold, color: Colors.white),
        ),
        backgroundColor: Colors.indigo,
        elevation: 0,
        centerTitle: true,
        iconTheme: const IconThemeData(color: Colors.white),
      ),
      body: SingleChildScrollView(
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            Container(
              height: 240,
              color: Colors.indigo.withOpacity(0.05),
              child: imageUrl.isNotEmpty
                  ? Image.network(
                imageUrl,
                fit: BoxFit.cover,
                errorBuilder: (context, error, stackTrace) {
                  return Center(
                    child: Icon(
                      Icons.person,
                      size: 80,
                      color: Colors.indigo.withOpacity(0.5),
                    ),
                  );
                },
              )
                  : Center(
                child: Icon(
                  Icons.person,
                  size: 80,
                  color: Colors.indigo.withOpacity(0.5),
                ),
              ),
            ),
            Transform.translate(
              offset: const Offset(0, -20),
              child: Container(
                margin: const EdgeInsets.symmetric(horizontal: 16),
                padding: const EdgeInsets.all(20),
                decoration: BoxDecoration(
                  color: Colors.white,
                  borderRadius: BorderRadius.circular(20),
                  border: Border.all(color: Colors.grey.shade200),
                  boxShadow: [
                    BoxShadow(
                      color: Colors.black.withOpacity(0.04),
                      blurRadius: 10,
                      offset: const Offset(0, 4),
                    ),
                  ],
                ),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Row(
                      mainAxisAlignment: MainAxisAlignment.spaceBetween,
                      children: [
                        Expanded(
                          child: Text(
                            student['name']?.toString() ?? 'N/A',
                            style: const TextStyle(
                              fontSize: 22,
                              fontWeight: FontWeight.bold,
                              color: Colors.black87,
                            ),
                          ),
                        ),
                        Container(
                          padding: const EdgeInsets.symmetric(
                            horizontal: 10,
                            vertical: 6,
                          ),
                          decoration: BoxDecoration(
                            color: gpaColor.withOpacity(0.1),
                            borderRadius: BorderRadius.circular(8),
                          ),
                          child: Text(
                            'GPA: ${student['gpa'] ?? 'N/A'}',
                            style: TextStyle(
                              fontSize: 14,
                              color: gpaColor,
                              fontWeight: FontWeight.bold,
                            ),
                          ),
                        ),
                      ],
                    ),
                    const SizedBox(height: 6),
                    Text(
                      student['degreeProgramName'] ?? 'Degree Program N/A',
                      style: const TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w600,
                        color: Colors.indigo,
                      ),
                    ),
                    const Padding(
                      padding: EdgeInsets.symmetric(vertical: 16),
                      child: Divider(height: 1, color: Colors.grey),
                    ),
                    _buildInfoRow(
                      Icons.email_outlined,
                      'Email',
                      student['email']?.toString() ?? 'N/A',
                    ),
                    const SizedBox(height: 12),
                    _buildInfoRow(
                      Icons.phone_outlined,
                      'Phone',
                      student['phone']?.toString() ?? 'N/A',
                    ),
                    const SizedBox(height: 12),
                    _buildInfoRow(
                      Icons.school_outlined,
                      'Academic Standing',
                      student['standing']?.toString() ?? 'N/A',
                    ),
                    const SizedBox(height: 12),
                    _buildInfoRow(
                      Icons.verified_user_outlined,
                      'Enrollment Status',
                      enrollmentStatusText,
                    ),
                  ],
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }

  Widget _buildInfoRow(IconData icon, String label, String value) {
    return Row(
      children: [
        Container(
          padding: const EdgeInsets.all(8),
          decoration: BoxDecoration(
            color: Colors.indigo.withOpacity(0.08),
            borderRadius: BorderRadius.circular(8),
          ),
          child: Icon(icon, size: 20, color: Colors.indigo),
        ),
        const SizedBox(width: 14),
        Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text(
              label,
              style: TextStyle(fontSize: 12, color: Colors.grey[500]),
            ),
            const SizedBox(height: 2),
            Text(
              value,
              style: const TextStyle(
                fontSize: 14,
                fontWeight: FontWeight.w600,
                color: Colors.black87,
              ),
            ),
          ],
        ),
      ],
    );
  }
}