import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { addressesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.worksOrderTimings.item,
    itemTypes.worksOrderTimings.actionType,
    itemTypes.worksOrderTimings.uri,
    actionTypes,
    config.appRoot
);
