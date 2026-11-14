import 'package:flutter/material.dart';
import '../../App2/data/CourseService.dart';

class CourseProvider extends ChangeNotifier {
  final CourseService courseService = CourseService();

  List<dynamic> courses = [];
  bool isLoading = false;
  String? error;

  Future<void> fetchCourses() async {
    isLoading = true;
    error = null;
    notifyListeners();

    try {
      courses = await courseService.getCourses();
    } catch (e) {
      error = 'Failed to load courses: $e';
    } finally {
      isLoading = false;
      notifyListeners();
    }
  }
}