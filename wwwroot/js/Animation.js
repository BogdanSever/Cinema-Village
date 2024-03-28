let logIn = document.getElementById('logIn');
let transitionDiv = document.getElementById('transition');
let active = 0;
let repeatPasswordInput = document.getElementById('repeatPasswordInput')
let repeatPasswordTitle = document.getElementById('repeatPasswordTitle')
let FormTitle = document.getElementById('FormTitle')
let FirstName = document.getElementById('FirstName')
let FirstNameInput = document.getElementById('firstNameInput')
let LastName = document.getElementById('LastName')
let LastNameInput = document.getElementById('lastNameInput')
let BtnSubmitFormSignUpLogin = document.getElementById('btnSubmitForm');
let LogInFormDiv = document.getElementById('LogIn');
let SignUpFormDiv = document.getElementById('SignUp');

logIn.addEventListener('click', change);

function change() {

    if (active == 0) {
        active = 1;

        if (window.innerWidth >= 650) {
            anime({

                targets: '#transition',
                translateX: '325',
                duration: '1000'

            }
            )
        } else {

            anime({
                targets: '#transition',
                translateX: '200',
                duration: '1000'

            }
            )

        }

        anime({
            targets: '#SignUp',
            translateX: '-170',
            duration: '1000'
        }
        )
        SignUpFormDiv.classList.add("hidden");
        LogInFormDiv.classList.remove("hidden");
        logIn.classList.remove('ml-10')
        logIn.classList.add('mr-10')
        transitionDiv.classList.remove('rounded-l-full');
        transitionDiv.classList.add('rounded-r-full');
        logIn.innerHTML = "<- Sign Up"

    } else {
        active = 0;
        anime({
            targets: '#transition',
            translateX: '0',
            duration: '1000'

        },
        )

        anime({
            targets: '#LogIn',
            translateX: '0',
            duration: '1000'
        })
        SignUpFormDiv.classList.remove("hidden");
        LogInFormDiv.classList.add("hidden");
        logIn.classList.add('ml-10')
        logIn.classList.remove('mr-10')
        transitionDiv.classList.remove('rounded-r-full');
        transitionDiv.classList.add('rounded-l-full');
        logIn.innerHTML = "Log In ->"
    }
}
