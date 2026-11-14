import 'package:flutter/material.dart';
import '../../App2/data/degreeProgramService.dart';

class DegreeProgramProvider extends ChangeNotifier {
  final DegreeProgramService degreeProgramService = DegreeProgramService();

  List<dynamic> degreePrograms = [];
  bool isLoading = false;
  String? error;

  Future<void> fetchDegreePrograms() async {
    isLoading = true;
    error = null;
    notifyListeners();

    try {
      degreePrograms = await degreeProgramService.getDegreePrograms();
    } catch (e) {
      error = 'Failed to load degree programs: $e';
    } finally {
      isLoading = false;
      notifyListeners();
    }
  }
}