import {Line} from 'react-chartjs-2';
import React from "react";
import './Tournaments.css';
import Cookies from 'universal-cookie';
import axios from 'axios'

export class MainLeagueGraph extends React.Component{
    constructor(props){
        super(props);
        var cookies = new Cookies();
        this.state = {
            lastUpdate: -1,
            userID: cookies.get('userID'),
            sessionIDs:-1,
            sessionValues: [-1],
            pHistory : {
                labels: ["January", "February", "March", "April", "May", "June", "July"], //GET FROM API
                datasets: [{
                    label : 'Place History',
                    backgroundColor: '#0013ae',
                    fill: false,
                    borderColor: '#0013ae',
                    data: [0, 10, 5, 2, 20, 30, 45], //NEED TO GET FROM API
                }],
            },
            graphOptions : {
                scales: {
                    yAxes: [
                        {ticks: 
                            {reverse: true}
                        }
                    ]
                },
        
            }
        }
    }
    
    async componentDidMount(){
        axios.get('http://localhost:5000/leagues/1/user/'+this.state.userID+'/history')
          .then((response) => {
              var sessions = response.data.value
              var sessionVal = sessions.map(sessions=>sessions.totalScore)
              this.setState({
                sessionIDs: sessions.map(sessions => sessions.sessionId), //GET FROM API
                sessionValues: sessionVal
              })    
          }, (error) => {
            console.log(error);
          });
          axios.get('http://localhost:5000/users/'+this.state.userID+'/sessions')
          .then((response) => {
              var sessions = response.data.value
              var inclSessions = this.state.sessionIDs
              var validSessions = sessions.filter((sessions) => inclSessions.includes(sessions.id));
              validSessions = validSessions.map(sessions => sessions.endDate)
              validSessions = validSessions.map(function(item) { 
                item = new Date(item)
                return item.toDateString();
              });
              this.setState({
                pHistory:{
                    labels: validSessions, //GET FROM API
                    datasets:[{
                        label : 'Place History',
                        backgroundColor: '#0013ae',
                        fill: false,
                        borderColor: '#0013ae',
                        data: this.state.sessionValues
                    }]
                }
              })
          }, (error) => {
            console.log(error);
          });
    }
    render(){
        return(
            <Line data={this.state.pHistory} options={this.state.graphOptions}/> 
        );
        
    }
} export default MainLeagueGraph