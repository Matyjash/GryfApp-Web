var firebaseConfig = {
    apiKey: "AIzaSyDG_Jhltdmbi6qAmbFHusUUNO0dGx3dPDM",
    authDomain: "ehhapp-5467e.firebaseapp.com",
    databaseURL: "https://ehhapp-5467e.firebaseio.com",
    projectId: "ehhapp-5467e",
    storageBucket: "ehhapp-5467e.appspot.com",
    messagingSenderId: "830836252123",
    appId: "1:830836252123:web:a861e8a8594dc9a7355bfe",
    measurementId: "G-QB4STXJBQL"
};

firebase.initializeApp(firebaseConfig);


firebase.auth().onAuthStateChanged(user => {
    if (user) {
        console.log("zalogowany");
        document.getElementById("login-form").style.display = 'none';
    } else {
        console.log("niezalogowany");
        if (window.location.href == "https://localhost:44389/Record" || window.location.href == "https://localhost:44389/Record/ShowRecord" || window.location.href == "https://localhost:44389/Record/ErrorInput"
            || window.location.href == "https://localhost:44389/Record/Create") {
            window.location.replace("https://localhost:44389/Login")
        }
    }


});

const bttnSignIn = document.getElementById('signIn');
const bttnSignOut = document.getElementById('signOut');
const txtEmail = document.getElementById('email');
const txtPassword = document.getElementById('password');

setTimeout(() => {
    bttnSignIn.addEventListener('click', e => {
        const auth = firebase.auth();
        const promise = auth.signInWithEmailAndPassword(txtEmail.value, txtPassword.value);
        promise.catch(e => console.log(e.message));
    });


}, 200);



setTimeout(() => {
    bttnSignOut.addEventListener('click', e => {
        const auth = firebase.auth();
        auth.signOut();
        document.getElementById("login-form").style.display = '';
    });


}, 200);
