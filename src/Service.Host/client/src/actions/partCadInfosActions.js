import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { partCadInfosActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.partCadInfos.item,
    itemTypes.partCadInfos.actionType,
    itemTypes.partCadInfos.uri,
    actionTypes,
    config.appRoot
);
