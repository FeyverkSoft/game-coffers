import * as React from 'react';
import { connect, DispatchProp } from 'react-redux';
import { Lang, GuildInfo, LangF, ITariffs, IGamerInfo, GamerRank, GuildBalanceReport, IGamersListView, IGuild } from '../_services';
import {
    Crumbs, BaseReactComp, СanvasBlock, Page, Grid,
    Col2, TariffView, UserView, MainView, BalanceView, GamerRowView
} from '../_components';

import { guildInstance, gamerInstance } from '../_actions';
import { IStore } from '../_helpers';
import { IHolded } from '../core';

interface IMainProps {
    isLoading?: boolean;
    userLoading?: boolean;
    user: IGamerInfo;
    tariffs: ITariffs;
    guildId?: string;
    guildInfo: GuildInfo;
    gamers: Array<IGamersListView>;
    balance: GuildBalanceReport & IHolded;
    userRank: GamerRank;
}

class Main extends BaseReactComp<IMainProps & DispatchProp<any>, any> {
    constructor(props: IMainProps & DispatchProp<any>) {
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
        if (this.props.guildId)
            this.props.dispatch(gamerInstance.GetGamers({ guildId: this.props.guildId }))
    }

    charactersGrid = () => {
        const { gamers } = this.props;
        return <СanvasBlock
            title={Lang("MAIN_PAGE_CHARACTERS_GRID")}
            type="success"
        >
            {
                gamers.map(g => {
                    return <GamerRowView key={g.id} gamer={g} />;
                })
            }
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
                        isLoading={this.props.isLoading}
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

const connectedMain = connect<{}, {}, {}, IStore>((state: IStore): IMainProps => {
    const { guild, tariffs, reports } = state.guild;
    const { currentGamer, gamersList } = state.gamers;
    return {
        isLoading: guild.holding,
        guildInfo: guild as GuildInfo,
        userRank: currentGamer.rank,
        user: currentGamer,
        tariffs: tariffs,
        guildId: state.session.guildId,
        balance: reports.balanceReport,
        gamers: Object.keys(gamersList).map(k => gamersList[k])
    };
})(Main);

export { connectedMain as MainController }; 