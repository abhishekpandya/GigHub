var AttendenceService = function () {
    var createAttendence = function (gigId, done, fail) {
        $.post("/api/attendences", { gigId: gigId })
                .done(done)
                .fail(fail);
    };

    var deleteAttendence = function (gigId, done, fail) {
        $.ajax({
            url: "/api/attendences/" + gigId,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    return {
        createAttendence: createAttendence,
        deleteAttendence: deleteAttendence
    }
}();