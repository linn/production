import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { workStationsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.workStations.item,
    itemTypes.workStations.actionType,
    itemTypes.workStations.uri,
    actionTypes,
    config.appRoot
);
