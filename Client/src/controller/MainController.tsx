import * as React from 'react';
import { connect, DispatchProp } from 'react-redux';
import { Lang, GuildInfo, LangF, ITariffs, DLang, IGamerInfo, GamerRank, GuildBalanceReport } from '../_services';
import {
    Crumbs, BaseReactComp, Form, Input, Button, СanvasBlock, Page, MaterialSelect, Grid,
    Col2, Col1, NamedValue, TariffView, UserView, MainView, BalanceView
} from '../_components';

import { guildInstance } from '../_actions';
import { IStore } from '../_helpers';
import { IHolded } from '../core';

interface IMainProps extends DispatchProp<any> {
    isLoading?: boolean;
    userLoading?: boolean;
    user: IGamerInfo;
    tariffs: ITariffs;
    guildId?: string;
    guildInfo: GuildInfo;
    balance: GuildBalanceReport & IHolded;
    userRank: GamerRank;
}

class Main extends BaseReactComp<IMainProps, any> {
    constructor(props: IMainProps) {
        super(props);
        this.state = {
        };
    }

    pageActions = (): React.ReactNode | string => {
        return <div>
        </div>;
    }

    componentDidMount() {
        if (this.props.guildInfo.id == '' && this.props.guildId)
            this.props.dispatch(guildInstance.GetGuild({ guildId: this.props.guildId }))
        if (this.props.guildId)
            this.props.dispatch(guildInstance.GetGuildBalanceReport({ guildId: this.props.guildId }))
    }

    charactersGrid = () => {
        return <СanvasBlock
            title={Lang("MAIN_PAGE_CHARACTERS_GRID")}
            type="success"
        >
        </СanvasBlock>;
    }

    render() {
        return <Page
            title={LangF("MAIN_PAGE", this.props.guildInfo.name)}
            breadcrumbs={[new Crumbs("./", LangF("MAIN_PAGE", this.props.guildInfo.name))]}
            pageActions={this.pageActions()}
        >
            <Grid
                direction="horizontal"
            >
                <Col2>
                    <MainView
                        guildInfo={this.props.guildInfo}
                    />
                </Col2>
                <Col2>
                    <BalanceView
                        balance={this.props.balance}
                    />
                </Col2>
                <Col2>
                    <TariffView
                        tariff={this.props.tariffs}
                        currentRole={this.props.userRank}
                        isLoading={this.props.isLoading}
                    />
                </Col2>
                <Col2>
                    <UserView
                        user={this.props.user}
                        isLoading={this.props.isLoading}
                        tax={this.props.tariffs[this.props.userRank].tax}
                    />
                </Col2>
            </Grid>
            {this.charactersGrid()}
        </Page>
    }
}

const connectedMain = connect<{}, {}, {}, IStore>((state: IStore) => {
    const { guild, tariffs, reports } = state.guild;
    const { currentGamer } = state.gamers;
    return {
        isLoading: guild.holding,
        guildInfo: guild,
        userRank: currentGamer.rank,
        user: currentGamer,
        tariffs: tariffs,
        guildId: state.session.guildId,
        balance: reports.balanceReport
    };
})(Main);

export { connectedMain as MainController }; 