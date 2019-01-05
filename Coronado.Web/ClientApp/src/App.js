import React, { Component, Fragment } from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Account from './components/account_page/Account';
import Categories from './components/categories/Categories';
import InvoicesPage from './components/invoices/InvoicesPage';
import { library } from '@fortawesome/fontawesome-svg-core'
import { faPiggyBank, faCreditCard, faHome, faHandHoldingUsd, 
  faDollarSign, faCar, faMoneyBillWave, faChartLine } from '@fortawesome/free-solid-svg-icons'
import CustomersPage from './components/customers/CustomersPage';
import ReportsPage from "./components/reports_page/ReportsPage";
import InvestmentsPage from "./components/investments_page/InvestmentsPage";
import { GoogleLogin } from 'react-google-login';

library.add(faPiggyBank, faCreditCard, faHome, faHandHoldingUsd, faDollarSign, faCar, faMoneyBillWave, faChartLine);

class App extends Component {
  constructor() {
    super();
    this.state = {
      isAuthenticated: false,
      token: '',
      user: null
    }
  }

  googleResponse = (e) => { 
    console.log(e);
    this.setState({isAuthenticated: true});
  };

  render() {
    let content = !this.state.isAuthenticated ?
    (
      <div> 
      <GoogleLogin
                        clientId="656317609073-47g9fnkeojgl1ga40bdmjgernmjq0l4k.apps.googleusercontent.com"
                        buttonText="Login"
                        onSuccess={this.googleResponse}
                        onFailure={this.googleResponse}
                    />
        </div>
    ) :
    (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/account/:accountId' render={props => <Account {...props} />} />
        <Route exact path='/categories' component={Categories} />
        <Route exact path='/invoices' component={InvoicesPage} />
        <Route exact path='/customers' component={CustomersPage} />
        <Route exact path='/reports' component={ReportsPage} />
        <Route exact path='/investments' component={InvestmentsPage} />
      </Layout>
    );
    return (
      <Fragment>
        {content}
      </Fragment>
    );
  }
}

export default App;