let nextBtn = document.getElementById("next");
let prevBtn = document.getElementById("prev");


let navItems = document.getElementById("navItems");

let img1 = document.getElementById("img1");
let img2 = document.getElementById("img2");
let img3 = document.getElementById("img3");

let join = document.getElementById("join");
let cls = document.getElementById("close");
let overlay = document.getElementById("overlay")

let k = 0;

if (join) {
    join.addEventListener("click", SignLogPop)
}

if (cls) {
    cls.addEventListener("click", SignLogPop)
}

nextBtn.addEventListener("click", changeNext);
prevBtn.addEventListener("click", changePrev);

function changeNext() {
    anime({

        targets: "#img1",
        translateX: [100, 0],
        duration: '300',
        easing: 'linear'


    })
    anime({

        targets: "#img2",
        translateX: [-20, 0],
        duration: '300',
        easing: 'linear'


    })
    anime({

        targets: "#img3",
        translateX: [-20, 0],
        duration: '300',
        easing: 'linear'


    })

    console.log(1)
    let a = img1.src;
    img1.src = img2.src;
    img2.src = img3.src;
    img3.src = a;


}
function changePrev() {
    anime({

        targets: "#img1",
        translateX: [-100, 0],
        duration: '300',
        easing: 'linear'


    })
    anime({

        targets: "#img2",
        translateX: [20, 0],
        duration: '200',
        easing: 'linear'


    })
    anime({

        targets: "#img3",
        translateX: [20, 0],
        duration: '200',
        easing: 'linear'


    })
    let a = img1.src;
    img1.src = img3.src;
    img3.src = img2.src;
    img2.src = a;

}

function SignLogPop() {

    if (k == 0) {
        overlay.classList.remove("hidden");
        overlay.classList.add("visible");
        navItems.classList.add("md:hidden");

        k = 1;

    }
    else {
        overlay.classList.add("hidden");
        overlay.classList.remove("visible");
        navItems.classList.remove("md:hidden");

        k = 0;
    }
}
