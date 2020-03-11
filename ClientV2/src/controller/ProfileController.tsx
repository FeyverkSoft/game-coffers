import * as React from 'react';
import { Breadcrumb, Layout, Col, Row, Statistic, Modal } from 'antd';
import { HomeOutlined } from '@ant-design/icons';
import { Lang, IProfile, ITax } from '../_services';
import style from './profile.module.scss';
import { connect } from 'react-redux';
import { IStore } from '../_helpers/store';
import { Content } from '../_components/Content/Content';
import { Link } from 'react-router-dom';
import { profileInstance } from '../_actions/profile/profile.actions';
import { IHolded } from '../core';
import { ProfileCard } from '../_components/Profile/ProfileCard';
import { Card } from '../_components/Base/Card';
import { TaxCard } from '../_components/Profile/TaxCard';
import { ICharacter } from '../_services/profile/ICharacter';
import { ProfileCharList } from '../_components/Profile/ProfileCharList';
import { AddCharDialog } from '../_components/Profile/AddCharDialog';

interface IProfileProps {
    Get: Function;
    GetTax: Function;
    GetCharacters: Function;
    SetMainChar(charId: string): void;
    DeleteChar(charId: string): void;
    profile: IProfile & IHolded;
    tax: ITax & IHolded;
    characters: Array<ICharacter> & IHolded;
}

interface IState {
    showAddModal: boolean;
}

export class _ProfileController extends React.Component<IProfileProps, IState> {
    constructor(props: IProfileProps) {
        super(props);
        this.state = {
            showAddModal: false
        }
    }

    componentDidMount() {
        if (this.props.profile === undefined)
            this.props.Get();
        if (this.props.tax === undefined || !this.props.tax.userId)
            this.props.GetTax();
        if (this.props.characters === undefined || this.props.characters.length === 0)
            this.props.GetCharacters();
    }

    setMainChar = (charId: string) => {
        let { characters } = this.props;
        if (characters.filter(_ => _.id === charId && _.isMain).length === 1)
            return;
        this.props.SetMainChar(charId);
    }

    deleteChar = (charId: string) => {
        this.props.DeleteChar(charId);
    }

    showAddCharModal = () => {
        this.setState({ showAddModal: !this.state.showAddModal });
    }

    render = () => {
        let { profile, tax, characters } = this.props;
        return <Content>
            <Breadcrumb>
                <Breadcrumb.Item>
                    <Link to={"/"} >
                        <HomeOutlined />
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
                    <Col xs={24} sm={24} md={14} lg={8} xl={6}>
                        <ProfileCard
                            profile={profile}
                            isLoading={profile.holding !== false}
                        />
                    </Col>
                    <Col xs={24} sm={12} md={5} lg={4} xl={2} >
                        <Card
                            loading={profile.holding}>
                            <Statistic
                                title={Lang('USER_CHAR_COUNT')}
                                valueStyle={{ color: '#3f8600' }}
                                value={profile.charCount}
                                precision={0} />
                        </Card>
                    </Col>
                    <Col xs={24} sm={12} md={5} lg={4} xl={2} >
                        <Card
                            loading={profile.holding}
                        >
                            <Statistic
                                title={Lang('USER_ROW_BALANCE')}
                                value={profile.balance}
                                precision={2}
                                suffix="G"
                            />
                        </Card>
                    </Col>
                    <Col xs={24} sm={12} md={6} lg={4} xl={3} >
                        <Card
                            loading={profile.holding}
                        >
                            <Statistic
                                title={Lang('USER_LOAN_AMOUNT')}
                                value={profile.activeLoanAmount}
                                precision={2}
                                suffix="G"
                            />
                        </Card>
                    </Col>
                    <Col xs={24} sm={12} md={6} lg={4} xl={3} >
                        <Card
                            loading={profile.holding}
                        >
                            <Statistic
                                title={Lang('USER_AMOUNT_PENALTIES')}
                                valueStyle={{ color: '#cf1322' }}
                                value={profile.activePenaltyAmount}
                                precision={2}
                                suffix="G"
                            />
                        </Card>
                    </Col>
                    <Col xs={24} sm={24} md={12} lg={24} xl={8} >
                        <TaxCard
                            loading={tax.holding}
                            tax={tax}
                            charCount={profile.charCount}
                        />
                    </Col>
                </Row>
                <Row gutter={[16, 16]}>
                    <Col xs={24} sm={24} md={24} lg={12} xl={12} >
                        <ProfileCharList
                            characters={characters}
                            loading={characters.holding}
                            SetMainChar={this.setMainChar}
                            DeleteChar={this.deleteChar}
                            AddChar={this.showAddCharModal}
                        />
                    </Col>
                </Row>
            </Layout>
            <AddCharDialog
                onClose={this.showAddCharModal}
                visible={this.state.showAddModal}
            />
        </Content>
    }
}

const connectedProfileController = connect<{}, {}, {}, IStore>(
    (state: IStore) => {
        const { profile, tax, characters } = state.profile;
        return { profile, tax, characters };
    },
    (dispatch: Function) => {
        return {
            Get: () => dispatch(profileInstance.Get()),
            GetTax: () => dispatch(profileInstance.GetTax()),
            GetCharacters: () => dispatch(profileInstance.GetChars()),
            SetMainChar: (charId: string) => dispatch(profileInstance.SetMainChar(charId)),
            DeleteChar: (charId: string) => dispatch(profileInstance.DeleteChar(charId)),
        }
    })(_ProfileController);

export { connectedProfileController as ProfileController };
