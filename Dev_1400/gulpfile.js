var gulp = require('gulp');
var sass = require('gulp-sass');

gulp.task('sass', function () {
    return gulp.src('./assets/sass/*.scss')
        .pipe(sass())
        .pipe(gulp.dest('./assets/css'));
});