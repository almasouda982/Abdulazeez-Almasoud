import 'package:flutter/material.dart';
import 'package:universitymobileapp/app/screens/bookScreen.dart';
import 'package:universitymobileapp/app/screens/courseBookScreen.dart';
import 'package:universitymobileapp/app/screens/coursePrerequisiteScreen.dart';
import 'package:universitymobileapp/app/screens/courseSectionScreen.dart';
import 'package:universitymobileapp/app/screens/coursesScreen.dart';
import 'package:universitymobileapp/app/screens/degreeProgramScreen.dart';
import 'package:universitymobileapp/app/screens/degreeRequirementScreen.dart';
import 'package:universitymobileapp/app/screens/detailScreens/courseBookDetailsScreen.dart';
import 'package:universitymobileapp/app/screens/enrollmentScreen.dart';
import 'package:universitymobileapp/app/screens/studentsScreen.dart';
import 'package:universitymobileapp/app/screens/waitlistEntryScreen.dart';

class HomeScreen extends StatelessWidget {
  HomeScreen(
      this.username,
      this.userId,
      this.email,
      {super.key});

  final String username;
  final int? userId;
  final String email;

  final dashboardItems = [
    {
      'title': 'Students',
      'icon': Icons.badge,
      'screen': const StudentsScreen(),
    },
    {
      'title': 'Enrollments',
      'icon': Icons.app_registration,
      'screen': const EnrollmentScreen(),
    },
    {
      'title': 'Courses',
      'icon': Icons.school,
      'screen': const CoursesScreen(),
    },

    {
      'title': 'Course Section',
      'icon': Icons.bookmark_add_outlined,
      'screen': const CourseSectionScreen(),
    },
    {
      'title': 'Course Prerequisites',
      'icon': Icons.bookmark_add,
      'screen': const CoursePrerequisiteScreen(),
    },
    {
      'title': 'Waitlist Entry',
      'icon': Icons.access_time_filled_outlined,
      'screen': const WaitlistEntryScreen(),
    },

    {
      'title': 'Degree Programs',
      'icon': Icons.sticky_note_2_outlined,
      'screen': const DegreeProgramScreen(),
    },
    {
      'title': 'Degree Requirements',
      'icon': Icons.sticky_note_2,
      'screen': const DegreeRequirementScreen(),
    },
    {
      'title': 'Books',
      'icon': Icons.book,
      'screen': const BooksScreen(),
    },


  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('University Dashboard'),
        backgroundColor: Colors.indigo,
      ),
      drawer: Drawer(
        child: ListView(
          children: [
            UserAccountsDrawerHeader(
              accountName: Text(username),
              accountEmail: Text(email),
              currentAccountPicture: const CircleAvatar(
                child: Icon(Icons.person, size: 40),
              ),
              decoration: const BoxDecoration(
                color: Colors.green,
              ),
            ),
            ListTile(
              leading: const Icon(Icons.home),
              title: const Text('Home'),
              onTap: () {
                Navigator.pop(context);
              },
            ),
          ],
        ),
      ),
      body: GridView.builder(
        padding: const EdgeInsets.all(12),
        gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
          crossAxisCount: 2,
          crossAxisSpacing: 10,
          mainAxisSpacing: 10,
          childAspectRatio: 1.0,
        ),
        itemCount: dashboardItems.length,
        itemBuilder: (context, index) {
          final item = dashboardItems[index];

          return InkWell(
            onTap: () {
              Navigator.push(
                context,
                MaterialPageRoute(
                  builder: (context) => item['screen'] as Widget,
                ),
              );
            },
            child: Card(
              elevation: 4,
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(15),
              ),
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Container(
                    padding: const EdgeInsets.all(16),
                    decoration: BoxDecoration(
                      color: Colors.indigo.withOpacity(0.1),
                      shape: BoxShape.circle,
                    ),
                    child: Icon(
                      item['icon'] as IconData,
                      size: 40,
                      color: Colors.indigo,
                    ),
                  ),
                  const SizedBox(height: 14),
                  Text(
                    item['title'] as String,
                    style: const TextStyle(
                      fontSize: 16,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ],
              ),
            ),
          );
        },
      ),
    );
  }
}