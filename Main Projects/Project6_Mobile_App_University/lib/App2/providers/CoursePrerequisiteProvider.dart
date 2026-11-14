import 'package:flutter/material.dart';
import '../../App2/data/coursePrerequisiteService.dart';

class CoursePrerequisiteProvider extends ChangeNotifier {
  final CoursePrerequisiteService prerequisiteService = CoursePrerequisiteService();

  List<dynamic> prerequisites = [];
  bool isLoading = false;
  String? error;

  Future<void> fetchCoursePrerequisites() async {
    isLoading = true;
    error = null;
    notifyListeners();

    try {
      prerequisites = await prerequisiteService.getCoursePrerequisites();
    } catch (e) {
      error = 'Failed to load course prerequisites: $e';
    } finally {
      isLoading = false;
      notifyListeners();
    }
  }
}