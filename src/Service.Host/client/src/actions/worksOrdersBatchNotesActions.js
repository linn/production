import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { worksOrdersActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.worksOrdersBathcNotes.item,
    itemTypes.worksOrdersBathcNotes.actionType,
    itemTypes.worksOrdersBathcNotes.uri,
    actionTypes,
    config.appRoot
);
