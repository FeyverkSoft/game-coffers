import * as React from 'react';
import { Breadcrumb, Icon, Layout, Col, Row } from 'antd';
import { Lang, IProfile } from '../_services';
import style from './profile.module.scss';
import { connect } from 'react-redux';
import { IStore } from '../_helpers/store';
import { Content } from '../_components/Content/Content';
import { Link } from 'react-router-dom';
import { profileInstance } from '../_actions/profile/profile.actions';
import { IHolded } from '../core';
import { ProfileCard } from '../_components/Profile/ProfileCard';

interface IProfileProps {
    Get: Function;
    profile: IProfile & IHolded;
}

export class _ProfileController extends React.Component<IProfileProps, any> {
    constructor(props: IProfileProps) {
        super(props);
        this.state = {
        }
    }

    componentDidMount() {
        if (this.props.profile == undefined)
            this.props.Get();
    }

    render = () => {
        return <Content>
            <Breadcrumb>
                <Breadcrumb.Item>
                    <Link to={"/"} >
                        <Icon type="home" />
                    </Link>
                </Breadcrumb.Item>
                <Breadcrumb.Item>
                    <Link to={"/profile"} >
                        {Lang("PROFILE_PAGE")}
                    </Link>
                </Breadcrumb.Item>
            </Breadcrumb>
            <Layout>
                <Row gutter={[16, 16]}>
                    <Col span={12} >
                        <ProfileCard
                            profile={this.props.profile}
                            isLoading={this.props.profile.holding !== false}
                        />
                    </Col>
                    <Col span={12} ></Col>
                </Row>
            </Layout>
        </Content>
    }
}

const connectedProfileController = connect<{}, {}, {}, IStore>(
    (state: IStore) => {
        const { profile } = state.profile;
        return { profile };
    },
    (dispatch: Function) => {
        return {
            Get: () => dispatch(profileInstance.Get()),
        }
    })(_ProfileController);

export { connectedProfileController as ProfileController };
