import React from 'react';
import { Router, Route, Switch, Link } from 'react-router-dom';
import 'antd/dist/antd.css';
import './App.css';
import { Menu, Layout } from 'antd';
import { Logo, Header } from './_components/Header/Header';
import { Private } from './_components/Private';
import { history, TryCatch } from './_helpers';
import { PrivateRoute } from './_components/PrivateRoute';
import { NotFoundController } from './controller/NotFoundController';
import { AuthController } from './controller/AuthController';
import { Lang } from './_services';
import { HeaderLink } from './_components/Header/HeaderLink';
import { ProfileButton } from './_components/ProfileButton/ProfileButton';


const { Content, Footer } = Layout;

export const App = ({ ...props }) => {
  return (
    <Layout className="layout">
      <Router
        history={history} >
        <Header>
          <Link to="./">
            <Logo />
          </Link>
          <Private>
            <Menu
              defaultSelectedKeys={['1']}
              defaultOpenKeys={['sub1']}
              mode="horizontal"
              theme="light"
            >
              <Menu.Item key="1">
                <HeaderLink to="/" exact>
                  {Lang('COFFERS')}
                </HeaderLink>
              </Menu.Item>
              <Menu.Item key="2">
                <HeaderLink to="/birthday" exact>
                  {Lang('BD')}
                </HeaderLink>
              </Menu.Item>
            </Menu>
            <ProfileButton />
          </Private>
        </Header>
        <Content >
          <TryCatch>
            <Switch>
              <Route path='/auth' component={AuthController} />
              {/*<Route path='/auth' component={AuthController} />
              <PrivateRoute path='/logout' component={LogOutController} />
              <PrivateRoute path='/birthday' component={BirthdayController} />
  <PrivateRoute path="/" component={MainController} />*/}
              <Route component={NotFoundController} />
            </Switch>
          </TryCatch>
        </Content>
      </Router>
      <Footer><div>© Peter 2019 - {(new Date()).getFullYear()}</div> <div>developed by Mazin Peter</div></Footer>
    </Layout>
  );
}

export default App;
