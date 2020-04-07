import React from "react";
import './Tournaments.css';
import MainLeagueLeaderboard from './MainLeagueLeaderboard'
import {Line} from 'react-chartjs-2';
import Cookies from 'universal-cookie';
import {
    BrowserRouter as Router,
    Switch,
    Route,
    Link
} from "react-router-dom";
export function MainLeaguePage() {
    const cookies = new Cookies();
    var userID  = cookies.get('userID'); 
    var userName = cookies.get('userName');
    var hPlace = 10 //NEED TO GET FROM API
    var cPlace = 10 //NEED TO GET FROM API
    var lastUpdate = "11/10/20"
    var pHistory = {
        labels: ["January", "February", "March", "April", "May", "June", "July"], //GET FROM API
        datasets: [{
            label : 'Place History',
            backgroundColor: '#0013ae',
            fill: false,
            borderColor: '#0013ae',
            data: [0, 10, 5, 2, 20, 30, 45], //NEED TO GET FROM API
        }],
    }
    var graphOptions = {
        scales: {
            yAxes: [
                {ticks: 
                    {reverse: true}
                }
            ]
        },

    }
    return (
        <div className="Tournament">
            <body>
                <div>
                    <div className="tournamentLeftSection">
                        <p><strong>Current Place</strong></p>
                        <p>{cPlace}</p>
                        <p><strong>Highest Place</strong></p>
                        <p>{hPlace}</p>
                        <p><strong>Last Updated</strong></p>
                        <p>{lastUpdate}</p>
                        <p>
                            <button className="session-button" >
                                <a href="/adminOptions/enterMainLeague" className="tournamentLink">Add Session Data</a>
                            </button>
                        </p>
                    </div>
                    <div className="tournamentRightSection">
                            <p><strong>Place History </strong></p>
                            <Line data={pHistory} options={graphOptions}/>                                   
                            <p>
                            <strong>Leaderboard </strong></p>
                            <MainLeagueLeaderboard/>
                    </div>
                </div>
            </body>
        </div>
    );
}