import 'package:flutter/material.dart';
import '../../../data/connection.dart';

class EnrollmentDetailsScreen extends StatelessWidget {
  final dynamic enrollment;

  const EnrollmentDetailsScreen(this.enrollment, {super.key});

  Color _getGradeColor(String? grade) {
    if (grade == null) return Colors.grey;
    final cleanGrade = grade.trim().toUpperCase();

    const greenGrades = ['A+', 'A', 'A-', 'B+', 'B'];
    const amberGrades = ['B-', 'C+', 'C', 'C-'];

    if (greenGrades.contains(cleanGrade)) {
      return Colors.green;
    } else if (amberGrades.contains(cleanGrade)) {
      return Colors.amber.shade700;
    } else {
      return Colors.red;
    }
  }

  String _formatStatus(dynamic rawStatus) {
    if (rawStatus == null) return 'N/A';
    if (rawStatus is bool) {
      return rawStatus ? 'Enrolled' : 'Not Enrolled';
    }
    final str = rawStatus.toString().toLowerCase();
    if (str == 'true') return 'Enrolled';
    if (str == 'false') return 'Not Enrolled';
    return rawStatus.toString();
  }

  @override
  Widget build(BuildContext context) {
    final gradeStr = enrollment['finalGrade']?.toString() ?? 'N/A';
    final gradeColor = _getGradeColor(gradeStr);
    final statusText = _formatStatus(enrollment['status']);

    return Scaffold(
      backgroundColor: Colors.grey[50],
      appBar: AppBar(
        title: const Text(
          'Enrollment Details',
          style: TextStyle(fontWeight: FontWeight.bold, color: Colors.white),
        ),
        backgroundColor: Colors.indigo,
        elevation: 0,
        centerTitle: true,
        iconTheme: const IconThemeData(color: Colors.white),
      ),
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(20),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              Container(
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
                            enrollment['lastName']?.toString() ?? 'N/A',
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
                            color: gradeColor.withOpacity(0.1),
                            borderRadius: BorderRadius.circular(8),
                          ),
                          child: Text(
                            'Grade: $gradeStr',
                            style: TextStyle(
                              fontSize: 14,
                              color: gradeColor,
                              fontWeight: FontWeight.bold,
                            ),
                          ),
                        ),
                      ],
                    ),
                    const SizedBox(height: 6),
                    Text(
                      'Section Number: ${enrollment['sectionNumber']?.toString() ?? 'N/A'}',
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
                      Icons.numbers_rounded,
                      'Letter Grade Points',
                      enrollment['letterGradePoints']?.toString() ?? 'N/A',
                    ),
                    const SizedBox(height: 12),
                    _buildInfoRow(
                      Icons.verified_user_outlined,
                      'Status',
                      statusText,
                    ),
                  ],
                ),
              ),
              const SizedBox(height: 20),
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
                borderRadius: BorderRadius.circular(14),
                child: Container(
                  padding: const EdgeInsets.all(16),
                  decoration: BoxDecoration(
                    color: Colors.indigo.withOpacity(0.05),
                    borderRadius: BorderRadius.circular(14),
                    border: Border.all(color: Colors.indigo.withOpacity(0.2)),
                  ),
                  child: Row(
                    children: [
                      const Icon(Icons.description_outlined, color: Colors.indigo),
                      const SizedBox(width: 12),
                      Expanded(
                        child: Text(
                          '${enrollment['lastName'] ?? 'Student'}.transcript',
                          style: const TextStyle(
                            fontSize: 15,
                            color: Colors.indigo,
                            fontWeight: FontWeight.bold,
                            decoration: TextDecoration.underline,
                          ),
                        ),
                      ),
                      const Icon(Icons.open_in_new_rounded, size: 16, color: Colors.indigo),
                    ],
                  ),
                ),
              ),
            ],
          ),
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