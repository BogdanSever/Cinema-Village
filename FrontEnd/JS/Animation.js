let logIn = document.getElementById('logIn');
let transitionDiv = document.getElementById('transition');
let active = 0;
let repeatPasswordInput = document.getElementById('repeatPassword')
let repeatPasswordTitle = document.getElementById('repeatPasswordTitle')
let FormTitle = document.getElementById('FormTitle')
let FullName = document.getElementById('FullName')
let FullNameInput = document.getElementById('FullNameInput')

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
        logIn.classList.remove('ml-10')
        logIn.classList.add('mr-10')
        transitionDiv.classList.remove('rounded-l-full');
        transitionDiv.classList.add('rounded-r-full');
        logIn.innerHTML = "<- Sign Up"
        repeatPasswordInput.classList.add("hidden");
        repeatPasswordTitle.classList.add("hidden");
        FullName.classList.add("hidden");
        FullNameInput.classList.add("hidden");
        FormTitle.innerHTML = "Log In"

    } else {
        active = 0;
        anime({
            targets: '#transition',
            translateX: '0',
            duration: '1000'

        },
        )

        anime({
            targets: '#SignUp',
            translateX: '0',
            duration: '1000'
        })
        logIn.classList.add('ml-10')
        logIn.classList.remove('mr-10')
        transitionDiv.classList.remove('rounded-r-full');
        transitionDiv.classList.add('rounded-l-full');
        logIn.innerHTML = "Log In ->"
        repeatPasswordInput.classList.remove("hidden");
        repeatPasswordTitle.classList.remove("hidden");
        FormTitle.innerHTML = "Sign Up"

    }
}
