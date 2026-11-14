import 'dart:convert';

import 'package:http/http.dart' as http;

import 'connection.dart';

class DegreeRequirementService {

  Future<List<dynamic>> getDegreeRequirements() async {

    final response = await http.get(
      Uri.parse(ConnectionString.degreeRequirement),
    );

    if (response.statusCode == 200) {
      return json.decode(response.body);
    } else {
      throw Exception('Failed to load degree requirements');
    }
  }


}