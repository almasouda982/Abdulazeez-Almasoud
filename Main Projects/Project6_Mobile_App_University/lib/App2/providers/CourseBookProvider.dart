import 'package:flutter/material.dart';
import '../../App2/data/courseBookService.dart';

class CourseBookProvider extends ChangeNotifier {
  final CourseBookService courseBookService = CourseBookService();

  List<dynamic> courseBooks = [];
  bool isLoading = false;
  String? error;

  Future<void> fetchBooksForCourse(int courseId) async {
    isLoading = true;
    error = null;
    notifyListeners();

    try {
      courseBooks = await courseBookService.getBooksForCourse(courseId);
    } catch (e) {
      error = 'Failed to load books for course: $e';
    } finally {
      isLoading = false;
      notifyListeners();
    }
  }

  Future<bool> addBookToCourse(int courseId, int bookId) async {
    try {
      await courseBookService.addBookToCourse(courseId, bookId);
      await fetchBooksForCourse(courseId);
      return true;
    } catch (e) {
      error = 'Failed to assign book: $e';
      notifyListeners();
      return false;
    }
  }

  Future<bool> removeBookFromCourse(int courseId, int bookId) async {
    try {
      await courseBookService.removeBookFromCourse(courseId, bookId);
      await fetchBooksForCourse(courseId);
      return true;
    } catch (e) {
      error = 'Failed to remove book: $e';
      notifyListeners();
      return false;
    }
  }
}