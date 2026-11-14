import 'package:flutter/material.dart';
import '../../App2/data/StudentService.dart';

class StudentProvider extends ChangeNotifier {
  final StudentService studentService = StudentService();

  List<dynamic> students = [];
  bool isLoading = false;
  String? error;

  Future<void> fetchStudents() async {
    isLoading = true;
    error = null;
    notifyListeners();

    try {
      students = await studentService.getStudents();
    } catch (e) {
      error = 'Failed to load students: $e';
    } finally {
      isLoading = false;
      notifyListeners();
    }
  }
}