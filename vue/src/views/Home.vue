<template>
  <div class="home" id="homePage">
    <body>
      <div id="myCarousel" class="carousel slide" data-bs-ride="carousel">
        <div class="carousel-indicators">
          <button
            type="button"
            data-bs-target="#myCarousel"
            data-bs-slide-to="0"
            class="active"
            aria-label="Slide 1"
            aria-current="true"
          ></button>
          <button
            type="button"
            data-bs-target="#myCarousel"
            data-bs-slide-to="1"
            aria-label="Slide 2"
            class=""
          ></button>
          <button
            type="button"
            data-bs-target="#myCarousel"
            data-bs-slide-to="2"
            aria-label="Slide 3"
            class=""
          ></button>
        </div>
        <div class="carousel-inner">
          <div class="carousel-item active" id="cItemOne">
            <div class="container">
              <div class="carousel-caption text-start">
                <h1>A tool for the whole family.</h1>
                <p>
                   Set up a group account for your loved ones to log their reading.
                </p>
                <p>
                  <a class="btn btn-lg btn-primary" href="/register">Sign up today</a>
                </p>
              </div>
            </div>
          </div>
          <div class="carousel-item" id="cItemTwo">
            <div class="container">
              <div class="carousel-caption">
                <h1>Want to learn more?</h1>
                <p>
                  Check out the information below to learn more about the application.
                </p>
                <p><a class="btn btn-lg btn-primary" href="#appMarketing">Learn more</a></p>
              </div>
            </div>
          </div>
          <div class="carousel-item" id="cItemThree">
            <div class="container">
              <div class="carousel-caption text-end">
                <h1>Already have an account?</h1>
                <p>
                  Log in to track your history!
                </p>
                <p>
                  <a class="btn btn-lg btn-primary" href="/login">Sign in</a>
                </p>
              </div>
            </div>
          </div>
        </div>
        <button
          class="carousel-control-prev"
          type="button"
          data-bs-target="#myCarousel"
          data-bs-slide="prev"
        >
          <span class="carousel-control-prev-icon" aria-hidden="true"></span>
          <span class="visually-hidden">Previous</span>
        </button>
        <button
          class="carousel-control-next"
          type="button"
          data-bs-target="#myCarousel"
          data-bs-slide="next"
        >
          <span class="carousel-control-next-icon" aria-hidden="true"></span>
          <span class="visually-hidden">Next</span>
        </button>
      </div>

      <!-- Marketing messaging and featurettes
  ================================================== -->
      <!-- Wrap the rest of the page in another container to center all the content. -->

      <div class="container marketing" id="appMarketing">
        
        <!-- START THE FEATURETTES -->

        <hr class="featurette-divider" />
        <div class="row featurette">
          <div class="col-md-7 order-md-2">
            <h2 class="featurette-heading">
              <span class="text-muted">Immerse.</span>
              An app desgined for families. 
            </h2>
            <p class="lead">
              For families who wish to track their reading.
            </p>
          </div>
          <div class="col-md-5 order-md-1">
            <svg
              class="bd-placeholder-img bd-placeholder-img-lg featurette-image img-fluid mx-auto"
              width="450"
              height="300"
              xmlns="http://www.w3.org/2000/svg"
              role="img"
              aria-label="Placeholder: 500x500"
              preserveAspectRatio="xMidYMid slice"
              focusable="false"
            >
              <title>Placeholder</title>
              <rect width="100%" height="100%" fill="#eee" />
              <image
                href="../assets/Immerse Logo.png"
                height="300"
                width="450"
              />
            </svg>
          </div>
        </div>
        <hr class="featurette-divider" />
        <div class="row featurette">
          <div class="col-md-7">
            <h2 class="featurette-heading">
              Immerse yourself in other worlds through reading. 
              <span class="text-muted">It’ll blow your mind.</span>
            </h2>
            <p class="lead">
              Add books to a family library for members to check out.
            </p>
          </div>
          <div class="col-md-5">
            <svg
              class="bd-placeholder-img bd-placeholder-img-lg featurette-image img-fluid mx-auto"
              width="450"
              height="300"
              xmlns="http://www.w3.org/2000/svg"
              role="img"
              aria-label="Placeholder: 500x500"
              preserveAspectRatio="xMidYMid slice"
              focusable="false"
            >
              <title>Placeholder</title>
              <rect width="100%" height="100%" fill="#eee" />
              <image
                href="../assets/ChildrenReadingOutside.jpg"
                height="300"
                width="450"
              />
            </svg>
          </div>
        </div>

        <hr class="featurette-divider" />

        <div class="row featurette">
          <div class="col-md-7 order-md-2">
            <h2 class="featurette-heading">
              Supports logging different ways of reading. 
              <span class="text-muted">See for yourself.</span>
            </h2>
            <p class="lead">
              Track whether you're reading in paper, on an eBook, audiobook, etc.
            </p>
          </div>
          <div class="col-md-5 order-md-1">
            <svg
              class="bd-placeholder-img bd-placeholder-img-lg featurette-image img-fluid mx-auto"
              width="450"
              height="300"
              xmlns="http://www.w3.org/2000/svg"
              role="img"
              aria-label="Placeholder: 500x500"
              preserveAspectRatio="xMidYMid slice"
              focusable="false"
            >
              <title>Placeholder</title>
              <rect width="100%" height="100%" fill="#eee" />
              <image
                href="../assets/failyReadingtogether.jpg"
                height="300"
                width="450"
                
              />
            </svg>
          </div>
        </div>

        <hr class="featurette-divider" />

        <div class="row featurette">
          <div class="col-md-7">
            <h2 class="featurette-heading">
              Track which books are more popular. <span class="text-muted">Checkmate.</span>
            </h2>
            <p class="lead">
              List out your family books and see which ones are getting read more and who's reading them.
            </p>
          </div>
          <div class="col-md-5">
            <svg
              class="bd-placeholder-img bd-placeholder-img-lg featurette-image img-fluid mx-auto"
              width="450"
              height="300"
              xmlns="http://www.w3.org/2000/svg"
              role="img"
              aria-label="Placeholder: 500x500"
              preserveAspectRatio="xMidYMid slice"
              focusable="false"
            >
              <title>Placeholder</title>
              <rect width="100%" height="100%" fill="#eee" />
              <image
                href="../assets/AsianBaby.jpg"
                height="300"
                width="450"
                
              />
            </svg>
          </div>
        </div>

        <hr class="featurette-divider" />

        <!-- /END THE FEATURETTES -->
      </div>
    </body>
  </div>
</template>

<script>
export default {
  name: "home",
};
</script>

<style scoped>
button {
  margin: 10px;
  border-radius: 10px;
}
/* GLOBAL STYLES
-------------------------------------------------- */
/* Padding below the footer and lighter body text */

body {
  padding-bottom: 3rem;
  color: #5a5a5a;
}

/* CUSTOMIZE THE CAROUSEL
-------------------------------------------------- */

/* Carousel base class */
.carousel {
  margin-bottom: 4rem;
}
/* Since positioning the image, we need to help out the caption */
.carousel-caption {
  bottom: 3rem;
  z-index: 10;
}

/* Declare heights because of positioning of img element */
.carousel-item {
  height: 32rem;
  background-color: #777;
  position: relative;
  background-position: center;
  background-size: cover;
  opacity: 150%;
}
#cItemOne{
   background-image:url(../assets/PickingABook.jpg);
}
#cItemTwo{
   background-image:url(../assets/TopDownReading.jpg);
}
#cItemThree{
   background-image:url(../assets/ExcitedKid.jpg);
}
</style>
