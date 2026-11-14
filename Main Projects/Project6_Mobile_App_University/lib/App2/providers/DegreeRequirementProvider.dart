import 'package:flutter/material.dart';
import '../../App2/data/degreeRequirementService.dart';

class DegreeRequirementProvider extends ChangeNotifier {
  final DegreeRequirementService requirementService = DegreeRequirementService();

  List<dynamic> degreeRequirements = [];
  bool isLoading = false;
  String? error;

  Future<void> fetchDegreeRequirements() async {
    isLoading = true;
    error = null;
    notifyListeners();

    try {
      degreeRequirements = await requirementService.getDegreeRequirements();
    } catch (e) {
      error = 'Failed to load degree requirements: $e';
    } finally {
      isLoading = false;
      notifyListeners();
    }
  }
}