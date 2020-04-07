import React, {Component} from "react";
import {BrowserRouter as Router, Route, Switch} from "react-router-dom";
import Cookies from 'universal-cookie';

/*import Redirect from "react-router-dom/es/Redirect";*/ // fucked


import './adminPages.css';

import {MLI} from "./MLI";
import {SLI} from "./SLI";
import {StartNewLeague} from "./StartNewLeague";


export function StartNewSession() {
  return (
    <div>
      <br/>

      <Router>
        <Switch>

          <Route exact path="/adminOptions/createNewSession">
            <div className="adminLeftSection">
              <p><b>CREATE NEW SESSION</b></p>
              <NewSessionForm/>
            </div>
            <div className="adminRightSection">
              list here
            </div>
          </Route>

{/*          <Route exact path="/adminOptions/enterSessionData/">
            <ThisHome />
          </Route>

          <Route exact path="/adminOptions/enterSessionData/enterMainLeague">
            <MLI />
          </Route>

          <Route exact path="/adminOptions/enterSessionData/enterSideLeague">
            <SLI />
          </Route>*/}

        </Switch>
      </Router>

      {/*<ThisHome/>*/}


    </div>
  )
}

function SendDataToAPI(pars) {
  console.log("SendDataToAPI");
  console.log(pars);

  const method = "POST";
  const url = "http://localhost:5000/sessions";
  /*let params = "{" +
      "\"email\": \"fart2@farty.com\"," +
      "\"name\": \"asdf2\"," +
      "\"password\": \"asdfasdf2\"" +
      "}";
  params = pars;*/
  let request = new XMLHttpRequest();
  request.open(method, url, true);
  request.setRequestHeader('Content-type', 'application/json');
  request.onload = function(){
    console.log(request.responseText)
    // TODO cookie set here
  };
  request.send(pars);

  const cookies = new Cookies();
  cookies.set('sessionID', '1', { path: '/' });

}

class NewSessionForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      startDate: '',
      endDate: '',
      frequency: '',
      venue: '',
      leagueID: '',
    };

    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleChange(event) {
    this.setState({ [event.target.name]: event.target.value});
    /*this.setState({value1: event.target.value1});*/ // KEEP ME FOR REFERENCE
  }

  handleSubmit(event) {
    console.log("huge fart");
    event.preventDefault();
    let valid = true;
    if (this.state.startDate.length === 0
      || this.state.endDate.length === 0
      || this.state.frequency.length === 0
      || this.state.venue.length === 0
      || this.state.leagueID.length === 0) {
      /*window.alert("Please fill in all fields");*/
      /*valid = false;*/ // TODO - UNCOMMENT ME WHEN DONE TESTING
    }

    if (valid) {
      SendDataToAPI("{" +
        "\"startDate\":\""+ this.state.startDate + "\"," +
        "\"endDate\":\""+ this.state.endDate + "\"," +
        "\"frequency\":\""+this.state.frequency + "\"," +
        "\"venue\":\""+this.state.venue + "\"," +
        "\"leagueID\":\""+this.state.leagueID+ "\"," +
        "}");
    }

    /*return <GoToDataInput/>*/
    /*return <Redirect to="/adminOptions/enterSessionData/"/>;*/
  }

  render() {
    return (
      <div>
        <form onSubmit={this.handleSubmit}>
          <input type="date" name="startDate" className="Input-box" placeholder="Start Date" value={this.state.startDate} onChange={this.handleChange}/> <br/>
          <input type="date" name="endDate" className="Input-box" placeholder="End Date" value={this.state.endDate} onChange={this.handleChange}/> <br/>
          <input type="number" name="frequency" className="Input-box" placeholder="Frequency" value={this.state.frequency} onChange={this.handleChange}/> <br/>
          <input type="text" name="venue" className="Input-box" placeholder="Venue" value={this.state.venue} onChange={this.handleChange}/> <br/>
          <input type="number" name="leagueID" className="Input-box" placeholder="League ID" value={this.state.leagueID} onChange={this.handleChange}/> <br/> <br/>
          <button type="submit" value="Submit" className="Login-button" >Create</button>
          <button type="submit" value="Submit" className="Login-button" ><a className="backLink" href="/adminOptions">Back</a></button>
        </form>
      </div>
    );
  }
}