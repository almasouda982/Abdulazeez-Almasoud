import 'package:flutter/material.dart';
import '../../App2/data/EnrollmentService.dart';

class EnrollmentProvider extends ChangeNotifier {
  final EnrollmentService enrollmentService = EnrollmentService();

  List<dynamic> enrollments = [];
  bool isLoading = false;
  String? error;

  Future<void> fetchEnrollments() async {
    isLoading = true;
    error = null;
    notifyListeners();

    try {
      enrollments = await enrollmentService.getEnrollments();
    } catch (e) {
      error = 'Failed to load enrollments: $e';
    } finally {
      isLoading = false;
      notifyListeners();
    }
  }
}