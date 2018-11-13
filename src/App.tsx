import * as React from 'react';
import './App.css';

class App extends React.Component {
  public render() {
    return (
      <div className="app">
        <header className="app-header">
          <div className="header-image">
            <img src="./images/header.jpg" style={{ "width": "100%" }} />
          </div>
          <h1 className="app-title">weredev</h1>
        </header>
        <div className="app-body">
          <div className="app-body-col">
            <h1 className="col-title">gamer</h1>
          </div>
          <div className="app-body-col">
            <h1 className="col-title">developer</h1>
          </div>
          <div className="app-body-col">
            <h1 className="col-title">world traveler</h1>
          </div>
        </div>
      </div>
    );
  }
}

export default App;
