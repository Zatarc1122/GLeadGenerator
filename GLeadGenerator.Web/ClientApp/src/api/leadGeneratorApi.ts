import { BusinessUser, CreateUser } from "../models";
import { api } from ".";

const usersUrlPrefix: string = "users";

export const getBusinessUsers = async () => {
  return api.get(`${usersUrlPrefix}/business`) as Promise<BusinessUser[]>;
};

export const createUser = async (user: CreateUser) => {
  return api.post(`${usersUrlPrefix}`, user) as Promise<any>;
};
