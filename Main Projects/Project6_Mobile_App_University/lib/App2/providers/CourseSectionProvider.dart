import 'package:flutter/material.dart';
import '../../App2/data/courseSectionService.dart';

class CourseSectionProvider extends ChangeNotifier {
  final CourseSectionService sectionService = CourseSectionService();

  List<dynamic> courseSections = [];
  bool isLoading = false;
  String? error;

  Future<void> fetchCourseSections() async {
    isLoading = true;
    error = null;
    notifyListeners();

    try {
      courseSections = await sectionService.getCourseSections();
    } catch (e) {
      error = 'Failed to load course sections: $e';
    } finally {
      isLoading = false;
      notifyListeners();
    }
  }
}