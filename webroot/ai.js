var run = false;
var interval = null;

var checkpoints = null;

$(document).ready(function() {

    $("#init").click(function() {
        
        $.ajax({
					url: "http://localhost:1337/init/" + $("#playerName").val() + "?format=json",
					cache: false
        })
        .done(function(json) {
            if(json == false) {
                $("#playerId").html("Too late!!! :(");
            } else {
                $("#playerId").html(json.id);
                window.checkpoints = json.checkpoints;
                //TODO map etc....
            }

        });
    });
    
    $("#countdownButton").click(function() {
        
        $.ajax({
					url: "http://localhost:1337/init?format=json",
					cache: false
        })
        .done(function(json) {
            $("#countdownLabel").html(json);
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
					/*$("#acc").html(json.acceleration);
					$("#str").html(json.steering);
					$("#spd").html(json.speed);*/
                    /*$("#acc").html(json.rotation[0]);
					$("#str").html(json.rotation[1]);
					$("#spd").html(json.rotation[2]);*/
                    $("#angle").html(json.rotation[1]);
                    var angle = Math.atan2(json.position[0] - checkpoints[json.nextCheckpoint][0], json.position[2] - checkpoints[json.nextCheckpoint][2]) * 180.0 / Math.PI + 180;
					$("#targetAngle").html(angle);
					
					var nextAcc = (50 - json.speed)/10;
                    //var nextSteer = (angle - json.rotation[1])/60;
                    var nextSteer = ((angle - json.rotation[1] + 180 + 360)%360 - 180)/60;
					$.ajax({
						url: "http://localhost:1337/input/" + $("#playerId").text() + "/" + nextAcc + "/" + nextSteer + "?format=json",
						cache: false
					});
				});
			}, 300);
			window.run = true;
		} else {
			clearInterval(window.interval);
			window.interval = null;
			window.run = false;
		}
	});

});
