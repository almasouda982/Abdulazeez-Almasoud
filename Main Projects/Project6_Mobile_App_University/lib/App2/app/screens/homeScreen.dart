import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:universitymobileapp/App2/app/screens/bookScreen.dart';
import 'package:universitymobileapp/App2/app/screens/courseBookScreen.dart';
import 'package:universitymobileapp/App2/app/screens/coursePrerequisiteScreen.dart';
import 'package:universitymobileapp/App2/app/screens/courseSectionScreen.dart';
import 'package:universitymobileapp/App2/app/screens/coursesScreen.dart';
import 'package:universitymobileapp/App2/app/screens/degreeProgramScreen.dart';
import 'package:universitymobileapp/App2/app/screens/degreeRequirementScreen.dart';
import 'package:universitymobileapp/App2/app/screens/enrollmentScreen.dart';
import 'package:universitymobileapp/App2/app/screens/studentsScreen.dart';
import 'package:universitymobileapp/App2/app/screens/waitlistEntryScreen.dart';
import '../../../App2/app/screens/loginScreen.dart';
import '../../providers/UserProvider.dart';
class HomeScreen extends StatelessWidget {
  const HomeScreen({super.key});

  final dashboardItems = const [
    {
      'title': 'Students',
      'icon': Icons.badge_outlined,
      'screen': StudentsScreen(),
    },
    {
      'title': 'Enrollments',
      'icon': Icons.app_registration_rounded,
      'screen': EnrollmentScreen(),
    },
    {
      'title': 'Courses',
      'icon': Icons.school_outlined,
      'screen': CoursesScreen(),
    },
    {
      'title': 'Course Sections',
      'icon': Icons.bookmark_add_outlined,
      'screen': CourseSectionScreen(),
    },
    {
      'title': 'Prerequisites',
      'icon': Icons.account_tree_outlined,
      'screen': CoursePrerequisiteScreen(),
    },
    {
      'title': 'Waitlist Entry',
      'icon': Icons.access_time_filled_outlined,
      'screen': WaitlistEntryScreen(),
    },
    {
      'title': 'Degree Programs',
      'icon': Icons.sticky_note_2_outlined,
      'screen': DegreeProgramScreen(),
    },
    {
      'title': 'Requirements',
      'icon': Icons.checklist_rounded,
      'screen': DegreeRequirementScreen(),
    },
    {
      'title': 'Books Catalog',
      'icon': Icons.menu_book_rounded,
      'screen': BooksScreen(),
    },
  ];
  @override
  Widget build(BuildContext context) {
    final userProvider = context.watch<UserProvider>();
    final username = userProvider.username;
    final email = userProvider.email;

    return Scaffold(
      backgroundColor: Colors.grey[550],
      appBar: AppBar(
        title: const Text(
          'University Dashboard',
          style: TextStyle(fontWeight: FontWeight.bold, color: Colors.white),
        ),
        backgroundColor: Colors.indigo,
        elevation: 0,
        centerTitle: true,
        iconTheme: const IconThemeData(color: Colors.white),
      ),
      drawer: Drawer(
        child: Column(
          children: [
            Expanded(
              child: ListView(
                padding: EdgeInsets.zero,
                children: [
                  UserAccountsDrawerHeader(
                    accountName: Text(
                      username,
                      style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 16),
                    ),
                    accountEmail: Text(email),
                    currentAccountPicture: CircleAvatar(
                      backgroundColor: Colors.white,
                      child: Text(
                        username.isNotEmpty ? username[0].toUpperCase() : 'A',
                        style: const TextStyle(
                          fontSize: 24,
                          fontWeight: FontWeight.bold,
                          color: Colors.indigo,
                        ),
                      ),
                    ),
                    decoration: const BoxDecoration(
                      color: Colors.indigo,
                    ),
                  ),
                  ListTile(
                    leading: const Icon(Icons.home_outlined, color: Colors.indigo),
                    title: const Text(
                      'Home Dashboard',
                      style: TextStyle(fontWeight: FontWeight.w600),
                    ),
                    onTap: () {
                      Navigator.pop(context);
                    },
                  ),
                ],
              ),
            ),

            const Divider(height: 1),
            ListTile(
              leading: const Icon(Icons.logout_rounded, color: Colors.redAccent),
              title: const Text(
                'Logout',
                style: TextStyle(fontWeight: FontWeight.w600, color: Colors.redAccent),
              ),
              onTap: () {
                context.read<UserProvider>().logout();

                Navigator.pushAndRemoveUntil(
                  context,
                  MaterialPageRoute(builder: (context) => const LoginScreen()),
                      (route) => false,
                );
              },
            ),
            const SizedBox(height: 16),
          ],
        ),
      ),
      body: SafeArea(
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Container(
              width: double.infinity,
              padding: const EdgeInsets.all(20.0),
              decoration: const BoxDecoration(
                color: Colors.indigo,
                borderRadius: BorderRadius.only(
                  bottomLeft: Radius.circular(24),
                  bottomRight: Radius.circular(24),
                ),
              ),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  const Text(
                    'Welcome back,',
                    style: TextStyle(color: Colors.white70, fontSize: 14),
                  ),
                  const SizedBox(height: 4),
                  Text(
                    username,
                    style: const TextStyle(
                      color: Colors.white,
                      fontSize: 22,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ],
              ),
            ),
            const SizedBox(height: 12),
            Expanded(
              child: GridView.builder(
                padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 12),
                gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
                  crossAxisCount: 2,
                  crossAxisSpacing: 14,
                  mainAxisSpacing: 14,
                  childAspectRatio: 1.15,
                ),
                itemCount: dashboardItems.length,
                itemBuilder: (context, index) {
                  final item = dashboardItems[index];

                  return Material(
                    color: Colors.transparent,
                    child: InkWell(
                      onTap: () {
                        Navigator.push(
                          context,
                          MaterialPageRoute(
                            builder: (context) => item['screen'] as Widget,
                          ),
                        );
                      },
                      borderRadius: BorderRadius.circular(16),
                      child: Container(
                        decoration: BoxDecoration(
                          color: Colors.white,
                          borderRadius: BorderRadius.circular(16),
                          border: Border.all(color: Colors.grey.shade200),
                          boxShadow: [
                            BoxShadow(
                              color: Colors.black.withOpacity(0.02),
                              blurRadius: 8,
                              offset: const Offset(0, 4),
                            ),
                          ],
                        ),
                        child: Column(
                          mainAxisAlignment: MainAxisAlignment.center,
                          children: [
                            Container(
                              padding: const EdgeInsets.all(12),
                              decoration: BoxDecoration(
                                color: Colors.indigo.withOpacity(0.08),
                                shape: BoxShape.circle,
                              ),
                              child: Icon(
                                item['icon'] as IconData,
                                size: 30,
                                color: Colors.indigo,
                              ),
                            ),
                            const SizedBox(height: 12),
                            Padding(
                              padding: const EdgeInsets.symmetric(horizontal: 8.0),
                              child: Text(
                                item['title'] as String,
                                textAlign: TextAlign.center,
                                style: const TextStyle(
                                  fontSize: 14,
                                  fontWeight: FontWeight.bold,
                                  color: Colors.black87,
                                ),
                              ),
                            ),
                          ],
                        ),
                      ),
                    ),
                  );
                },
              ),
            ),
          ],
        ),
      ),
    );
  }
}