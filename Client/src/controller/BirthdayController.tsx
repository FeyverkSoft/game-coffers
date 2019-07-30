import * as React from 'react';
import { Lang, IGamerInfo, IGamersListView } from '../_services';
import { render } from 'react-dom';
import { Page, Crumbs, Grid, Col3, СanvasBlock, Form, Col1, Button } from '../_components';
import { connect, DispatchProp } from 'react-redux';
import { IStore } from '../_helpers';
import { gamerInstance } from '../_actions';

interface IGamerView {
    id: string;
    name: string;
    birthday: string;
    count: number;
}

interface IMainProps {
    guildId?: string;
    isLoading: boolean;
    gamers: Array<IGamerView>;
}

export class _BirthdayController extends React.Component<IMainProps & DispatchProp<any>> {

    componentDidMount() {
        this.loadData();
    }

    loadData = () => {
        if (this.props.guildId) {
            this.props.dispatch(gamerInstance.GetGamers({ guildId: this.props.guildId }))
        }
    }

    render() {
        const { gamers, isLoading } = this.props;
        return (<Page
            title={Lang("BIRTHDAY_PAGE")}
            breadcrumbs={[new Crumbs("./birthday", Lang("BIRTHDAY_PAGE"))]}
        >
            <Grid
                align="center"
            >
                <Col1>
                    <СanvasBlock
                        isLoading={isLoading}
                        title={Lang("BIRTHDAY_TILE")}
                        type={"success"}
                    >
                        <Grid
                            align="center"
                        >
                            <Col1> {
                                gamers.map(g => {
                                    return (
                                        <div>
                                            <div>{g.name}</div>
                                            <div>{g.birthday}</div>
                                            <div>{g.count}</div>
                                        </div>
                                    )
                                })
                            }
                            </Col1>
                        </Grid>
                    </СanvasBlock>
                </Col1>
            </Grid>
        </Page>
        );
    }
}
const connectedBirthdayController = connect<{}, {}, {}, IStore>((state: IStore): IMainProps => {
    const { gamersList } = state.gamers;
    let d = new Date();
    return {
        isLoading: Object.keys(gamersList).length === 0,
        guildId: state.session.guildId,
        gamers: Object.keys(gamersList)
            .map(k => gamersList[k])
            .filter(g => !(g.status == 'Banned' || g.status == 'Left'))
            .map((_): IGamerView => {
                let f: any = new Date(_.dateOfBirth.getFullYear(), d.getMonth(), d.getDate());
                let n: any = new Date(_.dateOfBirth.getFullYear(), _.dateOfBirth.getMonth(), _.dateOfBirth.getDate());
                let res = Math.floor((f - n) / 86400000);
                return {
                    id: _.id,
                    name: _.name,
                    birthday: `${_.dateOfBirth.getDate()}-${_.dateOfBirth.getMonth() + 1}`,
                    count: res > 0 ? 365 - res : -1 * res
                }
            })
            .sort((a, b) => a.count - b.count)
    };
})(_BirthdayController);

export { connectedBirthdayController as BirthdayController }; 