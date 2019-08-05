import * as React from 'react';
import memoize from 'lodash.memoize';
import { Lang } from '../_services';
import { Page, Crumbs, Grid, Col3, CanvasBlock, Form, Col1, Button } from '../_components';
import { connect, DispatchProp } from 'react-redux';
import { IStore } from '../_helpers';
import { gamerInstance } from '../_actions';
import { UserBirthdayView } from '../_components/BirthdayView/UserBirthdayView';

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
                    <CanvasBlock
                        isLoading={isLoading}
                        title={Lang("BIRTHDAY_TILE")}
                        type={"success"}
                    >
                        <Grid
                            align="center"
                        >
                            <Col1> {
                                gamers.map(g => {
                                    return <UserBirthdayView
                                        key={g.id}
                                        id={g.id}
                                        name={g.name}
                                        birthday={g.birthday}
                                        dayCount={g.count}
                                    />
                                })
                            }
                            </Col1>
                        </Grid>
                    </CanvasBlock>
                </Col1>
            </Grid>
        </Page>
        );
    }
}

const MemGamers = memoize(gamersList => {
    let d = new Date();
    return Object.keys(gamersList)
        .map(k => gamersList[k])
        .filter(g => !(g.status == 'Banned' || g.status == 'Left'))
        .map((_): IGamerView => {
            let f: any = new Date(_.dateOfBirth.getFullYear(), d.getMonth(), d.getDate());
            let n: any = new Date(_.dateOfBirth.getFullYear(), _.dateOfBirth.getMonth(), _.dateOfBirth.getDate());
            let res = Math.floor((f - n) / 86400000);
            var mo = _.dateOfBirth.getMonth() + 1;
            return {
                id: _.id,
                name: _.name,
                birthday: `${_.dateOfBirth.getDate() > 9 ? _.dateOfBirth.getDate() : '0' + _.dateOfBirth.getDate()}-${mo > 9 ? mo : '0' + mo}`,
                count: res > 0 ? 365 - res : -1 * res
            }
        })
        .sort((a, b) => a.count - b.count)
}, it => JSON.stringify(it));

const connectedBirthdayController = connect<{}, {}, {}, IStore>((state: IStore): IMainProps => {
    const { gamersList } = state.gamers;
    return {
        isLoading: Object.keys(gamersList).length === 0,
        guildId: state.session.guildId,
        gamers: MemGamers(gamersList)
    };
})(_BirthdayController);

export { connectedBirthdayController as BirthdayController }; 