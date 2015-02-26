var run = false;
var interval = null;

$(document).ready(function() {

    $("#init").click(function() {
        
        $.ajax({
					url: "http://localhost:1337/init/" + $("#playerName").val() + "?format=json",
					cache: false
        })
        .done(function(json) {
            $("#playerId").html(json.id);
            //TODO map, checkpoints...
        });
    });
    
	$("#toggleButton").click(function() {
		if(window.run == false) {
			window.interval = window.setInterval(function(){
				$.ajax({
					url: "http://localhost:1337/status/" + $("#playerId").text() + "?format=json",
					cache: false
				})
				.done(function(json) {
					$("#acc").html(json.acceleration);
					$("#str").html(json.steering);
					$("#spd").html(json.speed);
					var nextAcc = (30 - json.speed)/10;
					$.ajax({
						url: "http://localhost:1337/input/" + $("#playerId").text() + "/" + nextAcc + "/-1.0?format=json",
						cache: false
					});
				});
			}, 500);
			window.run = true;
		} else {
			clearInterval(window.interval);
			window.interval = null;
			window.run = false;
		}
	});

	


});
