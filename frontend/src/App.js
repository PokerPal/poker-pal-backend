import React, { Component } from 'react';
// import { Link } from 'react-router'
import {
    BrowserRouter as Router,
    Switch,
    Route,
    Link
} from "react-router-dom";
// import { NativeRouter, Route, Link } from "react-router-native";
import logo from './bluffBathLogo.png';//'./logo.svg';
//bluffBathLogo.png
import './App.css';
import './Layout.css';
// import './Login';

class App extends Component {
    render() {
        return (
            <div className="universalTextColour">
                <div className="section">
                    <div className="header">

                        <div className="headerRight">
                            <h2 className="Page-header">Main League</h2>
                        </div>
                        <div className="headerLeft">
                            <img src={logo} className="App-logo-small" alt="App-logo-small"/>
                            {/*<h4>Bluff Bath</h4>*/}
                        </div>
                    </div>
                </div>
                <br></br>

                <Router>
                    <div className="navBar">
                        <Link to="/">  Home  </Link>
                        <Link to="/about">  About  </Link>
                        <Link to="/login">  Login  </Link>
                        User Profile |
                        Main League |
                        Side League |
                        Admin Options |
                        Settings
                    </div>
                    <Switch>
                        <Route exact path="/">
                            <Home />
                        </Route>

                        <Route path="/about">
                            <About />
                        </Route>

                        <Route path="/login">
                            <LoginTest />
                        </Route>
                    </Switch>
                </Router>

                <hr />

                <div className="section">

                    <div className="leftSection">
                        {/*<img src={logo} className="App-logo" alt="logo" width="500" height="500"/>*/}
                        <p>
                            left
                        </p>
                    </div>

                    <div className="rightSection">
                        <p>LOGIN </p>
                        <ul>
                            one
                            two
                            three
                        </ul>
                        {/*<form className="form1">*/}
                        {/*  <input type="text" id="fname" name="email" className="Input-box" placeholder="Email"></input>*/}
                        {/*  <br></br><br></br>*/}
                        {/*  <input type="password" id="pin" name="pw" className="Input-box" placeholder="Password"></input>*/}
                        {/*  <br></br><br></br>*/}
                        {/*  <input type="submit" value="Sign in" className="Login-button"></input>*/}
                        {/*</form>*/}
                    </div>
                    {/*<div className="leftSection">*/}
                    {/*  <body>*/}
                    {/*  left hand side*/}
                    {/*  </body>*/}
                    {/*</div>*/}

                    {/*<div className="rightSection">*/}
                    {/*  <body>*/}
                    {/*  right hand side*/}
                    {/*  </body>*/}
                    {/*</div>*/}

                </div>

            </div>

        );
    }
}

export default App;

function Home() {
    // Login();

    return (
        <div>
            <h2>Home</h2>
        </div>
    );
}

function About() {
    return (
        <div>
            <h2>About</h2>
        </div>
    );
}

function LoginTest() {
    return (
        <div className="App">
            <body>

                <div className="custom-header">
                    <b>Bluff Bath</b>
                </div>

                <div className="section">
                    <div className="leftSection">
                        <img src={logo} className="App-logo" alt="logo" width="500" height="500" />
                    </div>

                    <div className="rightSection">
                        <p>LOGIN </p>
                        <form className="form1">
                            <input type="text" id="fname" name="email" class="Input-box" placeholder="Email"></input>
                            <br /> <br />
                            <input type="password" id="pin" name="pw" class="Input-box" placeholder="Password"></input>
                            <br /> <br />
                            <input type="submit" value="Sign in" class="Login-button"></input>
                        </form>
                    </div>
                </div>

            </body>
        </div>
    );
}