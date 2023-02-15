abstract class IAuthService {
  bool isLoggedIn();
  String getUserUid();
  String getUserDisplayName();
  Future<void> signOut();
  Future<void> anonymousSignIn();
  Future<void> signUp(String email, String password, {String? username});
  Future<void> signIn(String email, String password);
}
