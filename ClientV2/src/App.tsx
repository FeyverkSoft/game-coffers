import React, { Suspense, lazy } from 'react';
import { Router, Route, Switch, Link } from 'react-router-dom';
import 'antd/dist/antd.css';
import { Menu, Layout, Skeleton } from 'antd';
import { Logo, Header } from './_components/Header/Header';
import { Private } from './_components/Private';
import { history, TryCatch } from './_helpers';
import { PrivateRoute, NotPrivateRoute } from './_components/PrivateRoute';
import { NotFoundController } from './controller/NotFoundController';
import { Lang } from './_services';
import { HeaderLink } from './_components/Header/HeaderLink';
import { LogOutController } from './controller/LogOutController';
import { ProfileButton } from './_components/Profile/ProfileButton';
const { Content, Footer } = Layout;

const load = (Component: any) => (props: any) => (
  <Suspense fallback={<Skeleton
    loading={true}
    active={true}
  />}>
    <Component {...props} />
  </Suspense>
);

const BirthdayController = load(lazy(() => import("./controller/BirthdayController")));
const AuthController = load(lazy(() => import("./controller/AuthController")));
const ProfileController = load(lazy(() => import("./controller/ProfileController")));
const CofferController = load(lazy(() => import("./controller/CofferController")));
const OperationsController = load(lazy(() => import("./controller/OperationsController")));
const ContractController = load(lazy(() => import("./controller/ContractController")));


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
            <Private hiddenFor={['demo']}>
              <Menu
                mode="horizontal"
                theme="light"
              >
                <Menu.Item key="/profile" >
                  <HeaderLink to="/profile">
                    {Lang('PROFILE')}
                  </HeaderLink>
                </Menu.Item>
                <Menu.Item key="/operations" >
                  <HeaderLink to="/operations">
                    {Lang('OPERATIONS')}
                  </HeaderLink>
                </Menu.Item>
                <Menu.Item key="/">
                  <HeaderLink to="/">
                    {Lang('COFFERS')}
                  </HeaderLink>
                </Menu.Item>
                <Menu.Item key="/birthday">
                  <HeaderLink to="/birthday">
                    {Lang('BD')}
                  </HeaderLink>
                </Menu.Item>
                <Menu.Item key="/contracts">
                  <HeaderLink to="/contracts">
                    {Lang('CONTRACTS')}
                  </HeaderLink>
                </Menu.Item>
              </Menu>
            </Private>
            <Private roles={['demo']}>
              <Menu
                mode="horizontal"
                theme="light"
              >
                <Menu.Item key="/profile" >
                  <HeaderLink to="/profile">
                    {Lang('PROFILE')}
                  </HeaderLink>
                </Menu.Item>
                <Menu.Item key="/contracts">
                  <HeaderLink to="/contracts">
                    {Lang('CONTRACTS')}
                  </HeaderLink>
                </Menu.Item>
              </Menu>
            </Private>
            <ProfileButton />
          </Private>
        </Header>
        <Content style={{ display: 'flex', flexDirection: 'row', flex: '1 1 100%', width: '90wv' }}>
          <TryCatch>
            <Switch>
              <NotPrivateRoute path='/auth' component={AuthController} />
              <PrivateRoute path='/profile' component={ProfileController} />
              <PrivateRoute path='/operations' component={OperationsController} />
              <PrivateRoute path='/logout' component={LogOutController} />
              <PrivateRoute path='/birthday' component={BirthdayController} />
              <PrivateRoute path="/contracts" component={ContractController} />
              <PrivateRoute path="/" component={CofferController} />
              <Route component={NotFoundController} />
            </Switch>
          </TryCatch>
        </Content>
      </Router>
      <Footer style={{ display: 'flex', justifyContent: 'space-between' }}>
        <div>© Peter 2019 - {(new Date()).getFullYear()}</div>
        <span>admin[гав-гав]guild-treasury.ru</span>
        <div>
          <a href="https://petr-mazin.ru/">developed by Mazin Peter</a>
        </div>
      </Footer>
    </Layout>
  );
}

export default App;
