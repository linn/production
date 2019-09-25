import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { worksOrdersActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.worksOrders.item,
    itemTypes.worksOrders.actionType,
    itemTypes.worksOrders.uri,
    actionTypes,
    config.appRoot
);
