import 'dart:convert';

import 'package:http/http.dart' as http;

import 'connection.dart';

class CoursePrerequisiteService {

  Future<List<dynamic>> getCoursePrerequisites() async {

    final response = await http.get(
      Uri.parse(ConnectionString.coursePrerequisite),
    );

    if (response.statusCode == 200) {
      return json.decode(response.body);
    } else {
      throw Exception('Failed to load course prerequisites');
    }
  }


}