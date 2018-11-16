import * as React from 'react';

class Index extends React.Component {
  public render() {
    return (
      <div>
        <div className="app-body-col">
          <h1 className="col-title">geek</h1>
          <p>
            I play games.  I roll dice.<br />
            And sometimes create things to help.
          </p>
          <p>
            <a href="/geek">Would you like to know more?</a>
          </p>
        </div>
        <div className="app-body-col">
          <h1 className="col-title">developer</h1>
          <p>
            I write software for paychecks.<br />
            And sometimes because I want stuff.
          </p>
          <p>
          <a href="/developer">Would you like to know more?</a>
          </p>
        </div>
        <div className="app-body-col">
          <h1 className="col-title">world traveler</h1>
          <p>
            Work to live, don't live to work.<br />
            And make sure there's pictures.
          </p>
          <p>
          <a href="/traveler">Would you like to see more?</a>
          </p>
        </div>
      </div>
    );
  }
}

export default Index;
