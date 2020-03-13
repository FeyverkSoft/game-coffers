import React from 'react';
import { Router, Route, Switch, Link } from 'react-router-dom';
import 'antd/dist/antd.css';
import { Menu, Layout } from 'antd';
import { Logo, Header } from './_components/Header/Header';
import { Private } from './_components/Private';
import { history, TryCatch } from './_helpers';
import { PrivateRoute } from './_components/PrivateRoute';
import { NotFoundController } from './controller/NotFoundController';
import { BirthdayController } from './controller/BirthdayController';
import { AuthController } from './controller/AuthController';
import { ProfileController } from './controller/ProfileController';
import { Lang } from './_services';
import { HeaderLink } from './_components/Header/HeaderLink';
import { LogOutController } from './controller/LogOutController';
import { ProfileButton } from './_components/Profile/ProfileButton';
import { OperationsController } from './controller/OperationsController';


const { Content, Footer } = Layout;

export const App = ({ ...props }) => {
  return (
    <Layout className="layout">
      <Router
        history={history}
      >
        <Header>
          <Link to="./">
            <Logo />
          </Link>
          <Private>
            <Menu
              /*defaultSelectedKeys={['/']}*/

              mode="horizontal"
              theme="light"
            >
              <Menu.Item key="/profile" >
                <HeaderLink to="/profile" exact>
                  {Lang('PROFILE')}
                </HeaderLink>
              </Menu.Item>
              <Menu.Item key="/operations" >
                <HeaderLink to="/operations" exact>
                  {Lang('OPERATIONS')}
                </HeaderLink>
              </Menu.Item>
              <Menu.Item key="/">
                <HeaderLink to="/" exact>
                  {Lang('COFFERS')}
                </HeaderLink>
              </Menu.Item>
              <Menu.Item key="/birthday">
                <HeaderLink to="/birthday" exact>
                  {Lang('BD')}
                </HeaderLink>
              </Menu.Item>
            </Menu>
            <ProfileButton />
          </Private>
        </Header>
        <Content style={{ display: 'flex', flexDirection: 'row', flex: '1 1 100%', width: '90wv' }}>
          <TryCatch>
            <Switch>
              <Route path='/auth' component={AuthController} />
              <PrivateRoute path='/profile' component={ProfileController} />
              <PrivateRoute path='/operations' component={OperationsController} />
              <PrivateRoute path='/logout' component={LogOutController} />
              <PrivateRoute path='/birthday' component={BirthdayController} />
              {/* <PrivateRoute path="/" component={MainController} />*/}
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
