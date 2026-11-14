import 'dart:convert';

import 'package:http/http.dart' as http;

import 'connection.dart';

class CourseSectionService {

  Future<List<dynamic>> getCourseSections() async {

    final response = await http.get(
      Uri.parse(ConnectionString.courseSection),
    );

    if (response.statusCode == 200) {
      return json.decode(response.body);
    } else {
      throw Exception('Failed to load course sections');
    }
  }


}