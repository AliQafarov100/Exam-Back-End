let left = document.querySelectorAll(".left");
let right = document.querySelectorAll(".rayt");
let slider = document.querySelector(".sliderItem");

left.forEach(lefts => {
    lefts.addEventListener("click",function(){
        
        let active = document.querySelector(".activeSlider");
        active.classList.remove("activeSlider");

        if(active.previousElementSibling == null){
           slider.lastElementChild.classList.add("activeSlider");
        }
        else{
            active.previousElementSibling.classList.add("activeSlider")
        }
    })
})

right.forEach(rayt => {
    rayt.addEventListener("click",function(){
        let active = document.querySelector(".activeSlider");
        active.classList.remove("activeSlider");

        if(active.nextElementSibling == null){
            slider.firstElementChild.classList.add("activeSlider");
         }
         else{
             active.nextElementSibling.classList.add("activeSlider")
         }
    })
})